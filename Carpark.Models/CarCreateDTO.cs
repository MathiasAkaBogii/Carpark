using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpark.Models
{
    public class CarCreateDTO
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public bool TicketBought { get; set; }
    }
}
