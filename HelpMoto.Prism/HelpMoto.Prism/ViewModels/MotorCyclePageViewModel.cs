using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpMoto.Prism.ViewModels
{
    public class MotorCyclePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private MotorcycleResponse _motorcycle;
        private DelegateCommand _editMotorcycleCommand;
        public MotorCyclePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.DetailMotorcycle;
            _navigationService = navigationService;
        }

        public DelegateCommand EditMotorcycleCommand => _editMotorcycleCommand ?? (_editMotorcycleCommand = new DelegateCommand(EditMotorcycleAsync));

        public MotorcycleResponse Motorcycle
        {
            get => _motorcycle;
            set => SetProperty(ref _motorcycle, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Motorcycle = JsonConvert.DeserializeObject<MotorcycleResponse>(Settings.MotorCycle);
        }

        private async void EditMotorcycleAsync()
        {
            var parameters = new NavigationParameters
            {
                { "motorcycle", Motorcycle }
            };

            await _navigationService.NavigateAsync("EditMotorCyclePage", parameters);
        }
    }
}
