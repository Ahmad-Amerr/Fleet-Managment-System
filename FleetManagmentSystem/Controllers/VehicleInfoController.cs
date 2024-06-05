using FleetManagmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using System.Threading.Tasks;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleInfoController : ControllerBase
    {
        private readonly IVehicleInformation _vehicleInfoService;

        public VehicleInfoController(IVehicleInformation vehicleInfoService)
        {
            _vehicleInfoService = vehicleInfoService;
        }

        [HttpPost]
    
    [Route("add")]
        public async Task<IActionResult> AddVehicleInformation([FromBody] GVAR request)
        {
            await _vehicleInfoService.AddVehicleInformationAsync(request);
            return Ok(new { message = "Vehicle information added successfully" });
        }
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateVehicleInformation([FromBody] GVAR request)
        {
            Console.WriteLine("request api :", request);
                await _vehicleInfoService.UpdateVehicleInformationAsync(request);
                return Ok(new { message = "Vehicle information updated successfully" });
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteVehicleInformation([FromBody] GVAR request)
        {
            Console.WriteLine("delete");
            await _vehicleInfoService.DeleteVehicleInformationAsync(request);
            return Ok(new { message = "Vehicle information deleted successfully" });
        }
    }
}
