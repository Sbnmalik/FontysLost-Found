using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstractions
{
    public interface IPostRepository
    {
        Task<int> InsertAsync(postDto entity);
        Task<List<postDto>> GetAllAsync();
        Task<postDto?> GetByIdAsync(int id);

        Task UpdateAsync(postDto entity);
        Task DeleteAsync(int id);
    }
}
