using BLL.DTOs;
using DAL.Models;
using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Repositories.ConcreteRepository;
using DAL.Repositories.InterfaceRepository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            if (await _repository.GetUserByEmailAsync(registerDto.Email) != null)
                return false;

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                RoleId = 2
            };

            _repository.AddUserAsync(user);
            return true;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _repository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new Exception("Invalid login credentials.");

            var token = GenerateJwtToken(user);

            await _repository.AddUserLogAsync(new UserConnectionLog
            {
                UserId = user.UserId,
                LoginTime = DateTime.Now,
                User = user
            });
            return token;
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var loginHistory = await _repository.GetUserLogAsync(userId);
            if (loginHistory != null)
            {
                _repository.UpdateUserLogoutAsync(loginHistory);
                return true;
            }
            return false;
        }

        private string GenerateJwtToken(User user)
        {
            // JWT token generation code
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHere123mertkarakaya");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateResetToken()
        {
            // Generate reset token code
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }

}
