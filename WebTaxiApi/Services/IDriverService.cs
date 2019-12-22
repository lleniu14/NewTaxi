using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver> GetByIdAsync(Guid id);
        Task CreateAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(Guid id);

    }
}
