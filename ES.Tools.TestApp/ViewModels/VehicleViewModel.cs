namespace ES.Tools.TestApp.ViewModels
{
  public abstract class VehicleViewModel
  {
    protected VehicleViewModel()
    { }

    public string Brand { get; set; }

    public abstract int Wheels { get; }
  }
}