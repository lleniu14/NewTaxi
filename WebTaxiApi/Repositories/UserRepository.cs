using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaxiAPIContext _taxiAPIContext;

        public UserRepository(TaxiAPIContext taxiAPIContext)
        {
            _taxiAPIContext = taxiAPIContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _taxiAPIContext.Users.ToListAsync();

        public async Task<User> GetAsync(Guid id)
            => await _taxiAPIContext.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string username)
            => await _taxiAPIContext.Users.SingleOrDefaultAsync(x => x.Username == username);

        public async Task AddAsync(User user)
        {
            await _taxiAPIContext.Users.AddAsync(user);
            await _taxiAPIContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _taxiAPIContext.Users.Remove(user);
            await _taxiAPIContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _taxiAPIContext.Users.Update(user);
            await _taxiAPIContext.SaveChangesAsync();
        }
    }
}
