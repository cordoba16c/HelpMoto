using Countries.Prism.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ApiCountries.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool isRunning;
        private bool _isEnabled;
        private DelegateCommand _countriesCommand;
        public List<CountryResponse> MyCountries { get; set; }
        private ObservableCollection<CountriesItemViewModel> _countries;
        public MainPageViewModel(INavigationService navigationService,
            IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Listar Countries";
            IsEnabled = true;
            this.LoadCountries();
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public ObservableCollection<CountriesItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private async void LoadCountries()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetListAsync<CountryResponse>(
                url,
                "rest",
                "/v2");
            if (!response.IsSuccess)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problem with user data, call support.", "Accept");
                return;
            }


            this.MyCountries = (List<CountryResponse>)response.Result;


            //  Countries = new ObservableCollection<CountryResponse>();
            var myListCountriesItemViewModel = MyCountries.Select(c => new CountriesItemViewModel
            {
                name = c.name,
                flag = c.flag,

            });
            this.Countries = new ObservableCollection<CountriesItemViewModel>(
                myListCountriesItemViewModel.OrderBy(c => c.name));

        }

    }
}
