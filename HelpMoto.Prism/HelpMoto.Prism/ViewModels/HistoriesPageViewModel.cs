using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HelpMoto.Prism.ViewModels
{
    public class HistoriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private MotorcycleResponse _motorcycle;
        private ObservableCollection<HistoryItemViewModel> _histories;
        private DelegateCommand _addHistoryCommand;

        public HistoriesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.History;
            Motorcycle = JsonConvert.DeserializeObject<MotorcycleResponse>(Settings.MotorCycle);
            LoadHistories();
        }

        public DelegateCommand AddHistoryCommand => _addHistoryCommand ?? (_addHistoryCommand = new DelegateCommand(AddHistory));

        public ObservableCollection<HistoryItemViewModel> Histories
        {
            get => _histories;
            set => SetProperty(ref _histories, value);
        }

        public MotorcycleResponse Motorcycle
        {
            get => _motorcycle;
            set => SetProperty(ref _motorcycle, value);
        }

        private void LoadHistories()
        {
            Histories = new ObservableCollection<HistoryItemViewModel>(Motorcycle.Histories.Select(h => new HistoryItemViewModel(_navigationService)
            {
                Id = h.Id,
                InicialDate = h.InicialDate,
                FinalDate = h.InicialDate,
                WorkshopType = h.WorkshopType,
                Description = h.Description,                
                Remarks = h.Remarks                
            }).ToList());
        }

        private async void AddHistory()
        {
            await _navigationService.NavigateAsync("HistoryPage");
        }
    }
}
