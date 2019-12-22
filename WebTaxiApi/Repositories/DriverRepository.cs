using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly TaxiAPIContext _taxiApiContext;

        public DriverRepository(TaxiAPIContext taxiApiContext)
        {
            _taxiApiContext = taxiApiContext;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await _taxiApiContext.Drivers.ToListAsync();

        public async Task<Driver> GetAsync(Guid id)
            => await _taxiApiContext.Drivers.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Driver driver)
        {
            await _taxiApiContext.Drivers.AddAsync(driver);
            await _taxiApiContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Driver driver)
        {
            _taxiApiContext.Drivers.Update(driver);
            await _taxiApiContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Driver driver)
        {
            _taxiApiContext.Drivers.Remove(driver);
            await _taxiApiContext.SaveChangesAsync();
        }
    }
}
