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
        Task<int> InsertAsync(Post entity);
        Task<Post?> GetByIdAsync(int id);
    }
}
