using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class DriverOrderDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Distance { get; set; }
        public string Time { get; set; }
        public int SeatsAmount { get; set; }
    }
}
