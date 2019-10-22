using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Countries.Prism.ViewModels
{
    class CountryViewModel : ViewModelBase
    {
       
        private ObservableCollection<Border> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;
     

      
        public Country Country
        {
            get;
            set;
        }

        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { this.SetValue(ref this.borders, value); }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { this.SetValue(ref this.currencies, value); }
        }

        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { this.SetValue(ref this.languages, value); }
        }
        

        
        public CountryViewModel(Country Country)
        {
            this.Country = Country;
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.Country.Currencies);
            this.Languages = new ObservableCollection<Language>(this.Country.Languages);
        }
       

        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Country.Borders)
            {
                var Country = MainPageViewModel.GetInstance().CountrysList.
                                        Where(l => l.Alpha3Code == border).
                                        FirstOrDefault();
                if (Country != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = Country.Alpha3Code,
                        Name = Country.Name,
                    });
                }
            }
        }
       
    }
}
