using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpMoto.Prism.ViewModels
{
    public class MotorCycleItemViewModel : MotorcycleResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMotorcycleCommand;

        public MotorCycleItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMotorcycleCommand => _selectMotorcycleCommand ?? (_selectMotorcycleCommand = new DelegateCommand(SelectMotorCycle));

        private async void SelectMotorCycle()
        {
            Settings.MotorCycle = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("MotorcycleTabbedPage");
        }
    }
}
