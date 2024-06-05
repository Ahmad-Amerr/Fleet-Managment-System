using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagmentSystem.Models
{       
    public class Geofence
        {
            [Key]
            public long GeofenceID { get; set; }

            [Required]
            public string GeofenceType { get; set; }

            [Required]
            public long AddedDate { get; set; }

            [Required]
            public string StrokeColor { get; set; }

            [Required]
            public double StrokeOpacity { get; set; }

            [Required]
            public double StrokeWeight { get; set; }

            [Required]
            public string FillColor { get; set; }

            [Required]
            public double FillOpacity { get; set; }

        }
    

}
