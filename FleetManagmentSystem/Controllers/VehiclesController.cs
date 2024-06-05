using FleetManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FleetManagmentSystem.Services;

namespace ClassLibrary1.src.src.Controllers
{
    [ApiController]
    [Route("vehicle/[controller]")]

    public class VehiclesController : ControllerBase
    {
        private readonly IVehicle _vehicleRepository;

        public VehiclesController(IVehicle vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] GVAR  gvar)
        {
            await _vehicleRepository.AddVehicleAsync(gvar);
            return Ok();
        }


        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAllVehicles()
        {
            GVAR result = await _vehicleRepository.GetAllVehiclesAsync();
            if (result.DicOfDT.TryGetValue("Vehicles", out DataTable dt))
            {
                Console.WriteLine("Vehicles DataTable:");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["VehicleID"]}, {row["VehicleNumber"]}, {row["VehicleType"]}, {row["LastDirection"]}, {row["LastStatus"]}, {row["LastAddress"]}, {row["LastLatitude"]}, {row["LastLongitude"]}");
                }
                return Ok(result);
            }
            return NotFound("No vehicles found.");
        }
        [HttpPost]
        [Route("GetDetails")]
        public async Task<IActionResult> GetVehicleDetails([FromBody] GVAR request)
        {
            Console.WriteLine("details",request);
            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                int.TryParse(tags["VehicleID"], out var vehicleID))
            {
                GVAR result = await _vehicleRepository.GetVehicleDetailsAsync(vehicleID);
                if (result.DicOfDT.TryGetValue("VehicleDetails", out DataTable dt))
                {
                    return Ok(result);
                }
                return NotFound($"No details found for vehicle ID {vehicleID}.");
            }
            return BadRequest("Invalid request data.");
        }


    }

}
