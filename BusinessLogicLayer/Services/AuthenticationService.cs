using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Data_Transfer_Objects;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> RegisterAsync(Register model)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var hash= HashPassword(model.Password, salt);

            var dto = new UserDTO
            {
                UserName = model.UserName,
                Email = model.EmailAdress,
                PasswordHash = hash,
                PasswordSalt = salt
            };
            return await _userRepository.InsertAsync(dto);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            var existing = await _userRepository.GetByUsernameAsync(userName);
            return existing != null;    
        }

        public async Task<UserDTO?> ValidateUserAsync(Login model)
        {
            var dto = await _userRepository.GetByUsernameAsync(model.UserName);
            if (dto == null) 
            return null;

            var hash = HashPassword(model.Password, dto.PasswordSalt);

            if (!hash.SequenceEqual(dto.PasswordHash))
            {
                return null;
            }
            return dto;
        }
        private static byte[] HashPassword(string password, byte[] salt)
        {
             using var derivedBytes = new Rfc2898DeriveBytes(password, salt, 100_00, HashAlgorithmName.SHA256);
            return derivedBytes.GetBytes(32);
        }

    }
}
