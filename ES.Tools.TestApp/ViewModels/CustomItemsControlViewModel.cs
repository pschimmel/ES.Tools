using System.Collections.Generic;

namespace ES.Tools.TestApp.ViewModels
{
  public class CustomItemsControlViewModel
  {
    public CustomItemsControlViewModel()
    {
      Items = new List<VehicleViewModel>
      {
        new CarViewModel { Brand="Peugeot", Model="308", Year=2020 },
        new BicycleViewModel { Brand = "Specialized" },
        new CarViewModel{ Brand="Ford", Model="Mustang", Year=1970 }
      };
    }

    public List<VehicleViewModel> Items { get; }
  }
}
