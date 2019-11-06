using HelpMoto.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpMoto.Prism.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.Profile;
        }
    }
}
