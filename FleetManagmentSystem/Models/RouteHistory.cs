using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagmentSystem.Models
{
    public class RouteHistory
    {
        [Key]
        public long RouteHistoryID { get; set; }

        [Required]
        public long VehicleID { get; set; }

        public int VehicleDirection { get; set; }

        [Required]
        public char Status { get; set; }

        public string VehicleSpeed { get; set; }

        [Required]
        public long Epoch { get; set; }

        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        
    }
}
