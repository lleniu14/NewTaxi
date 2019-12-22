using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class DriverOrderService : IDriverOrderService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverOrderService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DriverOrderDTO>> GetAllAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();
            var result = drivers.Select(x => _mapper.Map<DriverOrderDTO>(x));
            return result;
        }
    }
}