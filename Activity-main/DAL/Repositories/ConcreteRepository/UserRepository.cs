using DAL.DataContext;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.InterfaceRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories.ConcreteRepository
{
    public class UserRepository : IUserRepository
    {
        string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection"); ;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User user = null;
            string query = "SELECT * FROM MK_Users WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["FirstName"].ToString(),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString()
                        };
                    }
                }
            }

            return user;
        }

        public async Task AddUserAsync(User user)
        {
            string query = "INSERT INTO MK_Users (FirstName,LastName,Email, PasswordHash,RoleId) VALUES (@FirstName,@LastName,@Email, @PasswordHash,@RoleId)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task AddUserLogAsync(UserConnectionLog userConnectionLog)
        {
            string query = "INSERT INTO MK_UserConnectionLogs (UserId,LoginTime) VALUES (@UserId,@LoginTime)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userConnectionLog.UserId);
                command.Parameters.AddWithValue("@LoginTime", userConnectionLog.LoginTime);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<UserConnectionLog> GetUserLogAsync(int userId)
        {
            UserConnectionLog userConnectionLog = null;
            string query = "SELECT TOP 1 * FROM MK_UserConnectionLogs WHERE UserId = @UserId AND LogoutTime is null ORDER BY LoginTime DESC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        userConnectionLog = new UserConnectionLog
                        {
                            LogId = Convert.ToInt32(reader["LogId"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            LoginTime= Convert.ToDateTime(reader["LoginTime"]),
                        };
                    }
                }
            }

            return userConnectionLog;
            /*
            return await _context.MK_UserConnectionLogs.Where(lh => lh.UserId == userId && lh.LogoutTime == null)
                                                    .OrderByDescending(lh => lh.LoginTime)
                                                    .FirstOrDefaultAsync(); */
        }
        public async Task UpdateUserLogoutAsync(UserConnectionLog userConnectionLog)
        {
            string query = "UPDATE MK_UserConnectionLogs SET LogoutTime = @LogoutTime WHERE LogId=@LogId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LogoutTime", DateTime.Now);
                command.Parameters.AddWithValue("@LogId", userConnectionLog.LogId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

    }

}
