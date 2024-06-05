using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagmentSystem.Models
{
    public class Driver
    {
        [Key]
        public long DriverID { get; set; }

        [Required]
        public string DriverName { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        // Add more properties as needed
    }
}
