using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TaxiAPIContext _taxiApiContext;

        public OrderRepository(TaxiAPIContext taxiApiContext)
        {
            _taxiApiContext = taxiApiContext;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _taxiApiContext.Orders.ToListAsync();

        public async Task<Order> GetAsync(Guid id)
            => await _taxiApiContext.Orders.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Order order)
        {
            await _taxiApiContext.Orders.AddAsync(order);
            await _taxiApiContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _taxiApiContext.Orders.Update(order);
            await _taxiApiContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _taxiApiContext.Orders.Remove(order);
            await _taxiApiContext.SaveChangesAsync();
        }
    }
}
