using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return drivers;
        }

        public async Task<Driver> GetByIdAsync(Guid id)
        {
            var driver = await _driverRepository.GetAsync(id);
            return driver;
        }

        public async Task CreateAsync(Driver driver)
        {
            await _driverRepository.AddAsync(driver);
        }

        public async Task UpdateAsync(Driver driver)
        {
            var tmpDriver = await _driverRepository.GetAsync(driver.Id);

            if (driver == null)
                throw new AppException("Driver not found");

            if (!string.IsNullOrWhiteSpace(driver.FirstName))
                tmpDriver.FirstName = driver.FirstName;

            if (!string.IsNullOrWhiteSpace(driver.LastName))
                tmpDriver.LastName = driver.LastName;

            if (!string.IsNullOrWhiteSpace(driver.Pesel.ToString()))
                tmpDriver.Pesel = driver.Pesel;

            if (!string.IsNullOrWhiteSpace(driver.PhoneNumber.ToString()))
                tmpDriver.PhoneNumber = driver.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(driver.CarModel))
                tmpDriver.CarModel = driver.CarModel;

            if (!string.IsNullOrWhiteSpace(driver.RegistrationNumber))
                tmpDriver.RegistrationNumber = driver.RegistrationNumber;

            if (!string.IsNullOrWhiteSpace(driver.seatsAmount.ToString()))
                tmpDriver.seatsAmount = driver.seatsAmount;

            if (!string.IsNullOrWhiteSpace(driver.Salary.ToString()))
                tmpDriver.Salary = driver.Salary;

            await _driverRepository.UpdateAsync(tmpDriver);
        }

        public async Task DeleteAsync(Guid id)
        {
            var driver = await _driverRepository.GetAsync(id);
            if (driver != null)
            {
                await _driverRepository.DeleteAsync(driver);
            }
        }
    }
}
