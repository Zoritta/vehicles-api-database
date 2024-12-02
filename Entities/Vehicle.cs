namespace vehicles_api.Entities;

public class Vehicle
{
  public int Id { get; set; }
  public string RegistrationNumber { get; set; }
  public string Model { get; set; }
  public int ModelYear { get; set; }
  public string ImageUrl { get; set; }
  public int Mileage { get; set; }
  public int Value { get; set; }
  public string Description { get; set; }
  // Navigeringsegenskap som låter oss hämta information om aktuell tillverkare...
  public Manufacturer Manufacturer { get; set; }
}
