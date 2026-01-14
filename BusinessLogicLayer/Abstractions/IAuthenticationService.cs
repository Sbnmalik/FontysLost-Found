using BusinessLogicLayer.Models;
using BusinessLogicLayer.Data_Transfer_Objects;

namespace BusinessLogicLayer.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<bool> UserNameExistsAsync(string userName);
        public Task<int> RegisterAsync(Register model);
        public Task<UserDTO?> ValidateUserAsync(Login model);

    }
}
