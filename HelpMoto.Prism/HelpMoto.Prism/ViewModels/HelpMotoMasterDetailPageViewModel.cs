using HelpMoto.Common.Models;
using HelpMoto.Prism.Helpers;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HelpMoto.Prism.ViewModels
{
    public class HelpMotoMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public HelpMotoMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_motorcycle",
                    PageName = "MotorCyclesPage",
                    Title = Languages.MyMotorCycles
                },                

                new Menu
                {
                    Icon = "ic_map",
                    PageName = "MapPage",
                    Title = Languages.LocateWorkshop
                },

                new Menu
                {
                    Icon = "ic_person",
                    PageName = "ProfilePage",
                    Title = Languages.ModifyProfile
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Languages.Logout
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
