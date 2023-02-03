using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpark.Data.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(25)]
        public string Manufacturer { get; set; }

        [Required, MaxLength(25)]
        public string Model { get; set; }

        public bool TicketBought { get; set; }

        public DateTime? Created { get; set; }
    }
}
