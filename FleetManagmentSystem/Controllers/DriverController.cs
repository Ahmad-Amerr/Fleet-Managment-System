using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FleetManagmentSystem.Models;
using FleetManagmentSystem.Services;
using System.Data;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetDrivers()
        {
            GVAR result = await _driverService.GetDrivers();
            if (result.DicOfDT.TryGetValue("Drivers", out DataTable dt))
            {
                return Ok(result);
            }
            return NotFound("No drivers found.");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDriver([FromBody] GVAR request)
        {
            await _driverService.AddDriverAsync(request);
            return Ok(new { message = "Driver added successfully" });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDriver([FromBody] GVAR request)
        {
            await _driverService.UpdateDriverAsync(request);
            return Ok(new { message = "Driver updated successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDriver([FromBody] GVAR request)
        {
            await _driverService.DeleteDriverAsync(request);
            return Ok(new { message = "Driver deleted successfully" });
        }
        [HttpPost]
        [Route("GetDetails")]
        public async Task<IActionResult> GetDriverDetails([FromBody] GVAR request)
        {
            GVAR result = await _driverService.GetDriverDetails(request);
            if (result.DicOfDT.TryGetValue("DriverDetails", out DataTable dt))
            {
                return Ok(result);
            }
            return NotFound($"No details found for driver ID {request.DicOfDic["Tags"]["DriverID"]}.");
        }

    }
}
