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
        public MotorCyclePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Motor";
        }
    }
}
