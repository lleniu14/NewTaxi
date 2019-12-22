using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetAsync(id);
        }

        public async Task CreateAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            
        }

        public async Task UpdateAsync(Order order)
        {
            var tmpOrder = await _orderRepository.GetAsync(order.Id);

            if(order == null)
                throw new Exception("Order not found");

            if (!string.IsNullOrWhiteSpace(order.Name))
                tmpOrder.Name = order.Name;

            if (!string.IsNullOrWhiteSpace(order.PhoneNumber.ToString()))
                tmpOrder.PhoneNumber = order.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(order.NumberOfPassengers.ToString()))
                tmpOrder.NumberOfPassengers = order.NumberOfPassengers;

            if (!string.IsNullOrWhiteSpace(order.Address))
                tmpOrder.Address = order.Address;

            if (!string.IsNullOrWhiteSpace(order.Destination))
                tmpOrder.Destination = order.Destination;

            await _orderRepository.UpdateAsync(tmpOrder);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
            if (order != null)
                await _orderRepository.DeleteAsync(order);

        }
    }
}
