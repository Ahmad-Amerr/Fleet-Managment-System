using System.Data;
using System.Threading.Tasks;
using FleetManagmentSystem.Models;
using FleetManagmentSystem.Services;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FleetManagementAPI.Services
{
    public class VehicleInformationService : IVehicleInformation
    {
        private readonly string _connectionString;

        public VehicleInformationService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddVehicleInformationAsync(GVAR request)
        {
            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                int.TryParse(tags["DriverID"], out var driverID) &&
                tags.TryGetValue("VehicleMake", out var vehicleMake) &&
                tags.TryGetValue("VehicleModel", out var vehicleModel) &&
                long.TryParse(tags["PurchaseDate"], out var purchaseDate) &&
                tags.TryGetValue("VehicleNumber", out var vehicleNumber) &&
                tags.TryGetValue("VehicleType", out var vehicleType))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            var vehicleQuery = "INSERT INTO Vehicles (VehicleNumber, VehicleType) VALUES (@VehicleNumber, @VehicleType); SELECT LAST_INSERT_ID();";
                            using (var vehicleCommand = new MySqlCommand(vehicleQuery, connection, (MySqlTransaction)transaction))
                            {
                                vehicleCommand.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                                vehicleCommand.Parameters.AddWithValue("@VehicleType", vehicleType);
                                int vehicleID = Convert.ToInt32(await vehicleCommand.ExecuteScalarAsync());

                                var infoQuery = "INSERT INTO VehiclesInformations (VehicleID, DriverID, VehicleMake, VehicleModel, PurchaseDate) VALUES (@VehicleID, @DriverID, @VehicleMake, @VehicleModel, @PurchaseDate)";
                                using (var infoCommand = new MySqlCommand(infoQuery, connection, (MySqlTransaction)transaction))
                                {
                                    infoCommand.Parameters.AddWithValue("@VehicleID", vehicleID);
                                    infoCommand.Parameters.AddWithValue("@DriverID", driverID);
                                    infoCommand.Parameters.AddWithValue("@VehicleMake", vehicleMake);
                                    infoCommand.Parameters.AddWithValue("@VehicleModel", vehicleModel);
                                    infoCommand.Parameters.AddWithValue("@PurchaseDate", purchaseDate);
                                    await infoCommand.ExecuteNonQueryAsync();
                                }
                            }

                            await transaction.CommitAsync();
                        }
                        catch(Exception err)
                        {
                            Console.WriteLine(err.Message);
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
        }
        public async Task UpdateVehicleInformationAsync(GVAR request)
        {
            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                int.TryParse(tags["VehicleID"], out var vehicleID) &&
                int.TryParse(tags["DriverID"], out var driverID) &&
                tags.TryGetValue("VehicleMake", out var vehicleMake) &&
                tags.TryGetValue("VehicleModel", out var vehicleModel) &&
                long.TryParse(tags["PurchaseDate"], out var purchaseDate) &&
                tags.TryGetValue("VehicleNumber", out var vehicleNumber) &&
                tags.TryGetValue("VehicleType", out var vehicleType))
            {
                Console.WriteLine("VeihcleID", vehicleNumber);

                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            var vehicleQuery = "UPDATE Vehicles SET VehicleNumber = @VehicleNumber, VehicleType = @VehicleType WHERE VehicleID = @VehicleID";
                            using (var vehicleCommand = new MySqlCommand(vehicleQuery, connection, (MySqlTransaction)transaction))
                            {
                                vehicleCommand.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                                vehicleCommand.Parameters.AddWithValue("@VehicleType", vehicleType);
                                vehicleCommand.Parameters.AddWithValue("@VehicleID", vehicleID);
                                await vehicleCommand.ExecuteNonQueryAsync();
                            }

                            var infoQuery = "UPDATE VehiclesInformations SET DriverID = @DriverID, VehicleMake = @VehicleMake, VehicleModel = @VehicleModel, PurchaseDate = @PurchaseDate WHERE VehicleID = @VehicleID";
                            using (var infoCommand = new MySqlCommand(infoQuery, connection, (MySqlTransaction)transaction))
                            {
                                infoCommand.Parameters.AddWithValue("@DriverID", driverID);
                                infoCommand.Parameters.AddWithValue("@VehicleMake", vehicleMake);
                                infoCommand.Parameters.AddWithValue("@VehicleModel", vehicleModel);
                                infoCommand.Parameters.AddWithValue("@PurchaseDate", purchaseDate);
                                infoCommand.Parameters.AddWithValue("@VehicleID", vehicleID);
                                await infoCommand.ExecuteNonQueryAsync();
                            }

                            await transaction.CommitAsync();
                            Console.WriteLine("updated");
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
        }
        public async Task DeleteVehicleInformationAsync(GVAR request)
        {
            Console.WriteLine("delete");
            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                int.TryParse(tags["VehicleID"], out var vehicleID))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            var infoQuery = "DELETE FROM VehiclesInformations WHERE VehicleID = @VehicleID";
                            using (var infoCommand = new MySqlCommand(infoQuery, connection, (MySqlTransaction)transaction))
                            {
                                infoCommand.Parameters.AddWithValue("@VehicleID", vehicleID);
                                await infoCommand.ExecuteNonQueryAsync();
                            }

                            var vehicleQuery = "DELETE FROM Vehicles WHERE VehicleID = @VehicleID";
                            using (var vehicleCommand = new MySqlCommand(vehicleQuery, connection, (MySqlTransaction)transaction))
                            {
                                vehicleCommand.Parameters.AddWithValue("@VehicleID", vehicleID);
                                await vehicleCommand.ExecuteNonQueryAsync();
                            }

                            await transaction.CommitAsync();
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
        }
        }
}
