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
    [Route("[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<ActionResult> GetDriver()
        {
            var drivers = await _driverService.GetAllAsync();
            return Ok(drivers);
        }

        // GET: api/Driver/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _driverService.GetByIdAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // PUT: api/Driver/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutDriver([FromRoute] Guid id, [FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            driver.Id = id;
            try
            {
                await _driverService.UpdateAsync(driver);
                return NoContent();
            }
            catch (AppException ex)
            {
                return BadRequest(new { messege = ex.Message });
            }
        }

        // POST: api/Driver
        [HttpPost("add")]
        public async Task<IActionResult> PostDriver([FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _driverService.CreateAsync(driver);

            return Ok();
        }

        // DELETE: api/Driver/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDriver([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            await _driverService.DeleteAsync(id);

            return Ok(driver);
        }

        /*private bool DriverExists(Guid id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }*/
    }
}