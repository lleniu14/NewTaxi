using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public int NumberOfPassengers { get; set; }
        public string Address { get; set; }
        public string Destination { get; set; }
        //public DateTime Date { get; set; }
        public ICollection<DriverOrder> DriversOrders { get; set; }

    }
}
