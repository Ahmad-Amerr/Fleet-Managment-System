using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FleetManagmentSystem.Models;
using FleetManagmentSystem.Services;

namespace FleetManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteHistoryController : ControllerBase
    {
        private readonly IRouteHistoryService _routeHistoryService;

        public RouteHistoryController(IRouteHistoryService routeHistoryService)
        {
            _routeHistoryService = routeHistoryService;
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetRouteHistory([FromBody] GVAR request)
        {
        
           var gvar=   await _routeHistoryService.GetRouteHistoryAsync(request);
            return Ok(gvar) ;
        }
    
        [HttpPost("add")]
        public async Task<IActionResult> AddRouteHistory([FromBody] GVAR request)
        {
            await _routeHistoryService.AddRouteHistoryAsync(request);
            return Ok(new { message = "Route history added successfully" });
        }

        [HttpGet("geofence/GetAll")]
        public async Task<IActionResult> GetGeofences()
        {
            var gvar = await _routeHistoryService.GetGeofencesAsync();
            return Ok(gvar);
        }

        [HttpGet("circular-geofences")]
        public async Task<IActionResult> GetCircularGeofences()
        {
            var gvar = await _routeHistoryService.GetCircularGeofencesAsync();
            return Ok(gvar);
        }

        [HttpGet("rectangular-geofences")]
        public async Task<IActionResult> GetRectangularGeofences()
        {
            var gvar = await _routeHistoryService.GetRectangularGeofencesAsync();
            return Ok(gvar);
        }

        [HttpGet("polygon-geofences")]
        public async Task<IActionResult> GetPolygonGeofences()
        {
            var gvar = await _routeHistoryService.GetPolygonGeofencesAsync();
            return Ok(gvar);
        }
    }
}
