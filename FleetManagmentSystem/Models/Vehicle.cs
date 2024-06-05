using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagmentSystem.Models
{
    public class Vehicle
    {
        public long VehicleID { get; set; }

        [Required]
        public long VehicleNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string VehicleType { get; set; }

        public string LastDirection { get; set; }

        public string LastStatus { get; set; }

        public string LastAddress { get; set; }

        public double? LastLatitude { get; set; }

        public double? LastLongitude { get; set; }


    }
}
