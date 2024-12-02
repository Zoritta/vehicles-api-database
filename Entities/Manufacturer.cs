namespace vehicles_api.Entities;

public class Manufacturer
{
  public int Id { get; set; }
  public string Name { get; set; }
  // Navigeringsegenskap
  public List<Vehicle> Vehicles { get; set; }
}
