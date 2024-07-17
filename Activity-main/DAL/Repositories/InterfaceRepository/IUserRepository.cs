using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.InterfaceRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task AddUserLogAsync(UserConnectionLog userConnectionLog);
        Task<UserConnectionLog> GetUserLogAsync(int userId);
        Task UpdateUserLogoutAsync(UserConnectionLog userConnectionLog);
    }
}
