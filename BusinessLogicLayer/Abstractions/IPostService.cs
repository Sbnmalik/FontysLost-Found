using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstractions
{
    public  interface IPostService
    {
        Task<int> CreateAsync(postCreateDTO input);
        Task<postDto?>GetAsync(int id);
    }
}
