using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverOrderController : ControllerBase
    {
        private readonly IDriverOrderService _driverOrderService; 
        public DriverOrderController(IDriverOrderService driverOrderService)
        {
            _driverOrderService = driverOrderService;
        }
        [HttpGet]
        public async Task<ActionResult> GetDrivers()
        {
            var drivers = await _driverOrderService.GetAllAsync();
            return Ok(drivers);
        }

    }
}
