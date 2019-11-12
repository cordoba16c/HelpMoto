using HelpMoto.Prism.Helpers;
using Prism.Navigation;

namespace HelpMoto.Prism.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.LocateWorkshop;
        }
    }
}
