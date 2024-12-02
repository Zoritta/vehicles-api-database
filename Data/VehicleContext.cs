using Microsoft.EntityFrameworkCore;
using vehicles_api.Entities;

namespace vehicles_api.Data;

public class VehicleContext : DbContext
{
  // Vi behöver mappa klass till tabell...
  // Klassen Manufacturer kopplas/mappas till tabellen Manufacturers...
  public DbSet<Manufacturer> Manufacturers { get; set; }
  // Klassen Vehicle kopplas/mappas till tabellen Vehicles
  public DbSet<Vehicle> Vehicles { get; set; }

  // DbContextOptions innehåller anslutningssträng inklusive
  // vilken databas leverantör vi använder...
  public VehicleContext(DbContextOptions options) : base(options)
  {
  }
}
