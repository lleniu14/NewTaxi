using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models {
public class Driver
    {
         
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
         
        public string LastName { get; set; }
         
        public int Pesel { get; set; }
         
        public int PhoneNumber { get; set; }
         
        public string CarModel { get; set; }
         
        public string RegistrationNumber { get; set; }
         
        public int seatsAmount { get; set; }
         
        public int Salary { get; set; }
        public ICollection<DriverOrder> DriversOrders  { get; set; }
    }
}