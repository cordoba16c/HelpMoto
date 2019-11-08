using HelpMoto.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpMoto.Prism.ViewModels
{
    public class MotorCyclesPageViewModel : ViewModelBase
    {
        public MotorCyclesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.MyMotorCycles;
        }
    }
}
