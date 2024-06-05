using System.Data;
using System.Threading.Tasks;
using FleetManagmentSystem.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;

namespace FleetManagmentSystem.Services
{
    public class DriverService : IDriverService
    {
        private readonly string _connectionString;

        public DriverService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<GVAR> GetDrivers()
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("Drivers");

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT DriverID, DriverName, PhoneNumber FROM Driver";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            gvar.DicOfDT.TryAdd("Drivers", dataTable);
            return gvar;
        }
        public async Task<GVAR> GetDriverDetails(GVAR request)
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("DriverDetails");

            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                int.TryParse(tags["DriverID"], out var driverID))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
            SELECT 
                d.DriverName, d.PhoneNumber
            FROM Driver d
            WHERE d.DriverID = @DriverID";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", driverID);
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                gvar.DicOfDT.TryAdd("DriverDetails", dataTable);
            }

            return gvar;
        }


        public async Task AddDriverAsync(GVAR driverData)
        {
            var driverDic = driverData.DicOfDic["Tags"];
            var driverName = driverDic["DriverName"];
            var phoneNumber = long.Parse(driverDic["PhoneNumber"]);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO Driver (DriverName, PhoneNumber) VALUES (@DriverName, @PhoneNumber)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverName", driverName);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDriverAsync(GVAR driverData)
        {
            var driverDic = driverData.DicOfDic["Tags"];
            var driverID = int.Parse(driverDic["DriverID"]);
            var driverName = driverDic["DriverName"];
            var phoneNumber = long.Parse(driverDic["PhoneNumber"]);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Driver SET DriverName = @DriverName, PhoneNumber = @PhoneNumber WHERE DriverID = @DriverID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverID);
                    command.Parameters.AddWithValue("@DriverName", driverName);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteDriverAsync(GVAR driverData)
        {
            var driverDic = driverData.DicOfDic["Tags"];
            var driverID = int.Parse(driverDic["DriverID"]);

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Driver WHERE DriverID = @DriverID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverID", driverID);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
