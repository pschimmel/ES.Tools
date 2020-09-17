namespace ES.Tools.TestApp.ViewModels
{
  public class CarViewModel : VehicleViewModel
  {
    public string Model { get; set; }

    public int Year { get; set; }

    public override int Wheels => 4;
  }
}
