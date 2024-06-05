using System.Threading.Tasks;
using FleetManagmentSystem.Models;

namespace FleetManagmentSystem.Services
{
    public interface IRouteHistoryService
    {
        Task<GVAR> GetRouteHistoryAsync(GVAR request);
        Task AddRouteHistoryAsync(GVAR routeHistoryData);
        Task<GVAR> GetGeofencesAsync();
        Task<GVAR> GetCircularGeofencesAsync();
        Task<GVAR> GetRectangularGeofencesAsync();
        Task<GVAR> GetPolygonGeofencesAsync();
    }
}
