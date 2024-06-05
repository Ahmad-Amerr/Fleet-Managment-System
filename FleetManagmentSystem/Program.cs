using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FleetManagmentSystem.Services;
using FleetManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Connections;
using System.Net;
using System.Net.WebSockets;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

builder.Services.AddTransient<IVehicle, VehicleService>();
builder.Services.AddTransient<IVehicleInformation, VehicleInformationService>();
builder.Services.AddTransient<IRouteHistoryService, RouteHistoryService>();
builder.Services.AddTransient<IDriverService, DriverService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var ws = await context.WebSockets.AcceptWebSocketAsync();
        WebSocketManager.AddSocket(ws);

        while (true)
        {
            if (ws.State != WebSocketState.Open)
            {
                break;
            }

            var buffer = new byte[1024 * 4];
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await WebSocketManager.RemoveSocketAsync(ws);
                break;
            }
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});
app.MapControllers();
app.Run();
