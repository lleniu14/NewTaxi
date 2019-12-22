using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver> GetAsync(Guid id);
        Task AddAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(Driver driver);
    }
}
