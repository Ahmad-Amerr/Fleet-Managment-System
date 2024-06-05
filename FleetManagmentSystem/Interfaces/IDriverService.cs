using System.Threading.Tasks;
using FleetManagmentSystem.Models;

namespace FleetManagmentSystem.Services
{
    public interface IDriverService
    {
        Task<GVAR> GetDriverDetails(GVAR driverData);
        Task<GVAR> GetDrivers();
        Task AddDriverAsync(GVAR driverData);
        Task UpdateDriverAsync(GVAR driverData);
        Task DeleteDriverAsync(GVAR driverData);
    }
}
