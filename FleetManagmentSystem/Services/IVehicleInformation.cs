using FleetManagmentSystem.Models;

namespace FleetManagmentSystem.Services
{
    public interface IVehicleInformation
    {
        Task AddVehicleInformationAsync(GVAR request);
        Task UpdateVehicleInformationAsync(GVAR request);
        Task DeleteVehicleInformationAsync(GVAR request);
    }
}
