using ApiCountries.ViewModels;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Countries.Prism.ViewModels
{
    class CountriesItemViewModel : Country
    {
        public ICommand SelectCountryCommand
        {
            get
            {
                return new RelayCommand(SelectCountry);
            }
        }

        private async void SelectCountry()
        {
            MainPageViewModel.GetInstance().Country = new CountriesViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new CountriesTabbedPage());
        }
    }
 }
