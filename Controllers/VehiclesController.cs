using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicles_api.Data;

namespace vehicles_api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class VehiclesController : ControllerBase
{
  // Lista alla fordon som finns i databasen...
  private readonly VehicleContext _context;
  public VehiclesController(VehicleContext context)
  {
    _context = context;
  }

  [HttpGet()]
  // async nyckelordet gör att metoden kommer att köras asynkront
  // det vill säga inte blockera(icke-blockerande kod).
  // Task indikerar att vi kommer för eller senare returnera något.
  // Vi kan inte garantera när detta sker, men det kommer...
  public async Task<ActionResult> ListVehicles()
  {
    // await väntar in svaret ifrån vår fråga...
    var vehicles = await _context.Vehicles
      .Include(v => v.Manufacturer)
      .Select(vehicle => new // Projicering av data så det passar vårt ändamål...
      { // Vi skapar ett nytt C# objekt...
        vehicle.Id,
        regNo = vehicle.RegistrationNumber,
        manufacturer = vehicle.Manufacturer.Name,
        modelType = vehicle.Model,
        vehicleType = vehicle.Manufacturer.Name + " " + vehicle.Model,
        vehicle.ModelYear,
        vehicle.Mileage,
        vehicle.ImageUrl,
        vehicle.Value
      })
      .ToListAsync(); // Skickar frågan till databasen...
    return Ok(new { success = true, data = vehicles });
  }
}