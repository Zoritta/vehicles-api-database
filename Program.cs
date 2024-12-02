using Microsoft.EntityFrameworkCore;
using vehicles_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Lägga till en tjänst som kopplar oss till vår datalagrings provider
// Databasen!!!
builder.Services.AddDbContext<VehicleContext>(options =>
{
  options.UseSqlite("Data Source=vehicles.db");
});


builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();