using FleetManagmentSystem.Models;
using FleetManagmentSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagmentSystem.Services
{
    public interface IVehicle
    {
        Task AddVehicleAsync(GVAR gvar);
        Task<GVAR> GetAllVehiclesAsync();
        Task UpdateVehicleAsync(GVAR request);
        Task DeleteVehicleAsync(GVAR request);
        Task<GVAR> GetVehicleDetailsAsync(int vehicleID);
    }
}
