using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using FleetManagmentSystem.Models;
using FleetManagmentSystem.Services;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;

namespace FleetManagementAPI.Services
{
    public class VehicleService : IVehicle
    {
        private readonly string _connectionString;

        public VehicleService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task AddVehicleAsync(GVAR gvar)
        {
            if (gvar.DicOfDic.TryGetValue("Tags", out var tags) && tags.TryGetValue("VehicleNumber", out var vehicleNumber) && tags.TryGetValue("VehicleType", out var vehicleType))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO Vehicles (VehicleNumber, VehicleType) VALUES (@VehicleNumber, @VehicleType)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                        command.Parameters.AddWithValue("@VehicleType", vehicleType);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        public async Task UpdateVehicleAsync(GVAR request)
        {
            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                tags.TryGetValue("VehicleID", out var vehicleIDStr) &&
                int.TryParse(vehicleIDStr, out var vehicleID) &&
                tags.TryGetValue("VehicleNumber", out var vehicleNumber) &&
                tags.TryGetValue("VehicleType", out var vehicleType))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "UPDATE Vehicles SET VehicleNumber = @VehicleNumber, VehicleType = @VehicleType WHERE VehicleID = @VehicleID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VehicleID", vehicleID);
                        command.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                        command.Parameters.AddWithValue("@VehicleType", vehicleType);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        public async Task DeleteVehicleAsync(GVAR request)
        {
            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                tags.TryGetValue("VehicleID", out var vehicleIDStr) &&
                int.TryParse(vehicleIDStr, out var vehicleID))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM Vehicles WHERE VehicleID = @VehicleID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VehicleID", vehicleID);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
        public async Task<GVAR> GetAllVehiclesAsync()
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("Vehicles");
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT VehicleID, VehicleNumber, VehicleType, LastDirection, LastStatus, LastAddress, LastLatitude, LastLongitude FROM Vehicles";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            gvar.DicOfDT.TryAdd("Vehicles", dataTable);
            return gvar;
        }
        public async Task<GVAR> GetVehicleDetailsAsync(int vehicleID)
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("VehicleDetails");

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
        SELECT 
            v.VehicleNumber, v.VehicleType, d.DriverName, d.PhoneNumber,
            vi.VehicleMake, vi.VehicleModel, vi.PurchaseDate,
            v.LastDirection, v.LastStatus, v.LastAddress, v.LastLatitude, v.LastLongitude,
            rh.Epoch AS LastGPSTime, rh.VehicleSpeed AS LastGPSSpeed
        FROM Vehicles v
        LEFT JOIN VehiclesInformations vi ON v.VehicleID = vi.VehicleID
        LEFT JOIN Driver d ON vi.DriverID = d.DriverID
        LEFT JOIN RouteHistory rh ON v.VehicleID = rh.VehicleID
        WHERE v.VehicleID = @VehicleID
        ORDER BY rh.Epoch DESC";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleID);
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            gvar.DicOfDT.TryAdd("VehicleDetails", dataTable);
            return gvar;
        }
    }
}



