using System;
using System.Collections.Generic;
using System.Text;
using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using Prism.Commands;
using Prism.Navigation;

namespace HelpMoto.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenu));

        private async void SelectMenu()
        {
            if (PageName.Equals("LoginPage"))
            {
                Settings.IsRemembered = false;
                await _navigationService.NavigateAsync("/NavigationPage/LoginPage");
                return;
            }

            await _navigationService.NavigateAsync($"/HelpMotoMasterDetailPage/NavigationPage/{PageName}");

        }
    }
}
