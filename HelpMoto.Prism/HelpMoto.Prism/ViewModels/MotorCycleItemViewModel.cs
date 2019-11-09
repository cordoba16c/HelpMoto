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
        private DelegateCommand _selectPetCommand;

        public MotorCycleItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectPetCommand => _selectPetCommand ?? (_selectPetCommand = new DelegateCommand(SelectMotorCycle));

        private async void SelectMotorCycle()
        {
            Settings.MotorCycle = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("MotorCyclePage");
        }
    }
}
