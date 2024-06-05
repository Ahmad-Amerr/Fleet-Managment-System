using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using FleetManagmentSystem.Models;
using FleetManagmentSystem.Services;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace FleetManagementAPI.Services
{
    public class RouteHistoryService : IRouteHistoryService
    {
        private readonly string _connectionString;

        public RouteHistoryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<GVAR> GetRouteHistoryAsync(GVAR request)
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("RouteHistory");

            if (request.DicOfDic.TryGetValue("Tags", out var tags) &&
                int.TryParse(tags["VehicleID"], out var vehicleID) &&
                long.TryParse(tags["StartTime"], out var startTime) &&
                long.TryParse(tags["EndTime"], out var endTime))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                        SELECT VehicleID, VehicleDirection, Status, VehicleSpeed, Epoch, Address, Latitude, Longitude
                        FROM RouteHistory
                        WHERE VehicleID = @VehicleID AND Epoch BETWEEN @StartTime AND @EndTime
                        ORDER BY Epoch ASC";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VehicleID", vehicleID);
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@EndTime", endTime);
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }

            gvar.DicOfDT.TryAdd("RouteHistory", dataTable);
            return gvar;
        }
    

    public async Task AddRouteHistoryAsync(GVAR routeHistoryData)
        {
            if (routeHistoryData.DicOfDT.TryGetValue("RouteHistory", out var dataTable))
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var query = @"INSERT INTO RouteHistory (VehicleID, VehicleDirection, Status, VehicleSpeed, Epoch, Address, Latitude, Longitude)
                                      VALUES (@VehicleID, @VehicleDirection, @Status, @VehicleSpeed, @Epoch, @Address, @Latitude, @Longitude)";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@VehicleID", Convert.ToInt32(row["VehicleID"]));
                            command.Parameters.AddWithValue("@VehicleDirection", Convert.ToInt32(row["VehicleDirection"]));
                            command.Parameters.AddWithValue("@Status", Convert.ToChar(row["Status"]));
                            command.Parameters.AddWithValue("@VehicleSpeed", row["VehicleSpeed"].ToString());
                            command.Parameters.AddWithValue("@Epoch", Convert.ToInt64(row["Epoch"]));
                            command.Parameters.AddWithValue("@Address", row["Address"].ToString());
                            command.Parameters.AddWithValue("@Latitude", Convert.ToDouble(row["Latitude"]));
                            command.Parameters.AddWithValue("@Longitude", Convert.ToDouble(row["Longitude"]));
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    // Broadcast the new route history point
                    var gvar = new GVAR();
                    var broadcastDataTable = ConvertToDataTable(dataTable);
                    gvar.DicOfDT.TryAdd("RouteHistory", broadcastDataTable);

                    var gvarJson = JsonConvert.SerializeObject(gvar);
                    await WebSocketManager.BroadcastMessageAsync(gvarJson);
                }
            }
        }

        private DataTable ConvertToDataTable(DataTable dataTable)
        {
            var newDataTable = new DataTable();
            foreach (DataColumn col in dataTable.Columns)
            {
                newDataTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                var newRow = newDataTable.NewRow();
                newRow.ItemArray = row.ItemArray;
                newDataTable.Rows.Add(newRow);
            }

            return newDataTable;
        }

        public async Task<GVAR> GetPolygonGeofencesAsync()
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("PolygonGeofences");
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"SELECT GeofenceID, Latitude, Longitude 
                                 FROM PolygonGeofence";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            gvar.DicOfDT.TryAdd("PolygonGeofences", dataTable);
            return gvar;
        }

        public async Task<GVAR> GetRectangularGeofencesAsync()
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("RectangularGeofences");
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"SELECT GeofenceID, North, East, West, South 
                                 FROM RectangularGeofence";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            gvar.DicOfDT.TryAdd("RectangularGeofences", dataTable);
            return gvar;
        }

        public async Task<GVAR> GetCircularGeofencesAsync()
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("CircularGeofences");
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"SELECT GeofenceID, Radius, Latitude, Longitude 
                                 FROM CircularGeofence";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            gvar.DicOfDT.TryAdd("CircularGeofences", dataTable);
            return gvar;
        }

        public async Task<GVAR> GetGeofencesAsync()
        {
            var gvar = new GVAR();
            var dataTable = new DataTable("Geofences");
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Geofences";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            gvar.DicOfDT.TryAdd("Geofences", dataTable);
            return gvar;
        }
    }
}
