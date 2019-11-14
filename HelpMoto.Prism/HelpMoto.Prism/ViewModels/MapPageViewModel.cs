using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Common.Services;
using HelpMoto.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HelpMoto.Prism.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<WorkshopResponse> _workshop;

        public MapPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.LocateWorkshop;
            LoadWorkshop();
        }

        public ObservableCollection<WorkshopResponse> Workshop
        {
            get => _workshop;
            set => SetProperty(ref _workshop, value);
        }

        private async void LoadWorkshop()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {                
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CheckConnection,
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var response = await _apiService.GetListAsync<WorkshopResponse>(
                url,
                "/api",
                "/Workshops",
                "bearer",
                token.Token);

            
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Error Cargando los talleres",
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var workshop = (List<WorkshopResponse>)response.Result;
            Workshop = new ObservableCollection<WorkshopResponse>(workshop);
        }
    }
}
