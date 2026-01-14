using BusinessLogicLayer.Data_Transfer_Objects;

namespace BusinessLogicLayer.Abstractions
{
    public interface IUserRepository
    {
        Task<UserDTO?> GetByUsernameAsync(string userName);
        Task<int> InsertAsync(UserDTO user);
        Task<int> UpdateAsync(UserDTO user);
        Task<int> DeleteAsync(int id);  

    }
}
