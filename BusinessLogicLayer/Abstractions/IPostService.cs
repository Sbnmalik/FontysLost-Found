using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Abstractions
{
    public interface IPostService
    {
        Task<int> CreateAsync(postCreateDTO input);
        Task<Post?> OnGetAsync(int id);
        Task<List<Post>> GetAllAsync();

        Task UpdateAsync(postUpdatedDto input);
        Task DeleteAsync(int id);
        Task<Post?> GetByIdAsync(int id);
    }
}
