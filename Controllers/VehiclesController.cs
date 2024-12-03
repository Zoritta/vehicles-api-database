using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicles_api.Data;
using vehicles_api.Entities;

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
        vehicle.Model,
        vehicleType = vehicle.Manufacturer.Name + " " + vehicle.Model,
        vehicle.ModelYear,
        vehicle.Mileage,
        vehicle.ImageUrl,
        vehicle.Value
      })
      .ToListAsync(); // Skickar frågan till databasen...
    return Ok(new { success = true, data = vehicles });
  }

  [HttpGet("{id}")]
  public async Task<ActionResult> FindById(int id)
  {
    var vehicle = await _context.Vehicles
      .Where(v => v.Id == id)
      .Include(m => m.Manufacturer)
      .Select(vehicle => new
      {
        vehicle.Id,
        regNo = vehicle.RegistrationNumber,
        manufacturer = vehicle.Manufacturer.Name,
        vehicle.Model,
        vehicle.ModelYear,
        vehicle.Mileage,
        vehicle.ImageUrl,
        vehicle.Value
      })
    .SingleOrDefaultAsync();
    return Ok(new { success = true, data = vehicle });
  }

  [HttpGet("regno/{regNo}")]
  public async Task<ActionResult> FindByRegNu(string regNo)
  {
    var vehicle = await _context.Vehicles
      .Where(v => v.RegistrationNumber.ToLower() == regNo.ToLower())
      .Include(m => m.Manufacturer)
      .Select(vehicle => new
      {
        vehicle.Id,
        regNo = vehicle.RegistrationNumber,
        manufacturer = vehicle.Manufacturer.Name,
        vehicle.Model,
        vehicle.ModelYear,
        vehicle.Mileage,
        vehicle.ImageUrl,
        vehicle.Value
      })
    .SingleOrDefaultAsync();
    return Ok(new { success = true, data = vehicle });
  }

  [HttpPost()]
  public async Task<ActionResult> Add(Vehicle vehicle)
  {
    // TODO: prevent the repetative regNu to be added to the database
    var manufacturer = await _context.Manufacturers.FindAsync(vehicle.ManufacturerId);
    vehicle.Manufacturer = manufacturer;
    _context.Vehicles.Add(vehicle);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(FindById), new { id = vehicle.Id }, new
    {
      vehicle.Id,
      regNo = vehicle.RegistrationNumber,
      manufacturer = vehicle.Manufacturer.Name,
      vehicle.Model,
      vehicle.ModelYear,
      vehicle.Mileage,
      vehicle.ImageUrl,
      vehicle.Value
    });
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Update(int id, Vehicle vehicle)
  {
    var toUpdate = await _context.Vehicles.FindAsync(id);
    toUpdate.RegistrationNumber = vehicle.RegistrationNumber;
    toUpdate.Model = vehicle.Model;
    toUpdate.ImageUrl = vehicle.ImageUrl;
    toUpdate.Mileage = vehicle.Mileage;
    toUpdate.ModelYear = vehicle.ModelYear;
    toUpdate.Value = vehicle.Value;
    toUpdate.Description = vehicle.Description;

    await _context.SaveChangesAsync();
    return NoContent();
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> Patch(int id, Vehicle vehicle)
  {
    var toUpdate = await _context.Vehicles.FindAsync(id);
    toUpdate.RegistrationNumber = vehicle.RegistrationNumber;
    toUpdate.Mileage = vehicle.Mileage;
    toUpdate.ModelYear = vehicle.ModelYear;
    await _context.SaveChangesAsync();
    
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    var toDelete = await _context.Vehicles.FindAsync(id);
    _context.Vehicles.Remove(toDelete);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}