using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class DriverOrder
    {
        public Guid Id { get; set; }
        public virtual Driver Driver { get; set; }
        public Guid DriverId { get; set; }
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
