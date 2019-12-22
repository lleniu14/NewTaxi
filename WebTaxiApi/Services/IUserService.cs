using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Users;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user, string password);
        Task UpdateAsync(User user, string password = null);
        Task DeleteAsync(Guid id);
    }
}
