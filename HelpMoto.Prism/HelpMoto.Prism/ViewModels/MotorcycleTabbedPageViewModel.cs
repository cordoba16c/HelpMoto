using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpMoto.Prism.ViewModels
{
    public class MotorcycleTabbedPageViewModel : ViewModelBase
    {
        public MotorcycleTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var motorcycle = JsonConvert.DeserializeObject<MotorcycleResponse>(Settings.MotorCycle);
            Title = $"Motorcycle: {motorcycle.Brand} {motorcycle.Name}";
        }
    }
}
