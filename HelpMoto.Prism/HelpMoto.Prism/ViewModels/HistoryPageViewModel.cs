using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Common.Services;
using HelpMoto.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Prism.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private HistoryResponse _history;
        private MotorcycleResponse _motorcycle;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isEdit;
        private ObservableCollection<WorkshopTypeResponse> _workshopTypes;
        private WorkshopTypeResponse _workshopType;
        private DelegateCommand _saveCommand;

        public HistoryPageViewModel(INavigationService navigationService,
                                    IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public ObservableCollection<WorkshopTypeResponse> WorkshopTypes
        {
            get => _workshopTypes;
            set => SetProperty(ref _workshopTypes, value);
        }

        public WorkshopTypeResponse WorkshopType
        {
            get => _workshopType;
            set => SetProperty(ref _workshopType, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public HistoryResponse History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }

        public MotorcycleResponse MotorCycle
        {
            get => _motorcycle;
            set => SetProperty(ref _motorcycle, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("history"))
            {
                History = parameters.GetValue<HistoryResponse>("history");
                MotorCycle = parameters.GetValue<MotorcycleResponse>("motorcycle");
                IsEdit = true;
                Title = Languages.EditHistory;
            }
            else
            {
                History = new HistoryResponse { InicialDate = DateTime.Today, FinalDate = DateTime.Today };
                MotorCycle = parameters.GetValue<MotorcycleResponse>("motorcycle");
                IsEdit = false;
                Title = Languages.NewHistory;
            }

            LoadWorkshopTypesAsync();
        }

        private async void LoadWorkshopTypesAsync()
        {
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CheckConnection,
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var response = await _apiService.GetListAsync<WorkshopTypeResponse>(
                url,
                "/api",
                "/WorkshopTypes",
                "bearer",
                token.Token);

            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.WorkshopGetError,
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var workshopTypes = (List<WorkshopTypeResponse>)response.Result;
            WorkshopTypes = new ObservableCollection<WorkshopTypeResponse>(workshopTypes);

            if (!string.IsNullOrEmpty(History.WorkshopType))
            {
                WorkshopType = WorkshopTypes.FirstOrDefault(wt => wt.Name == History.WorkshopType);
            }
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            //var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);

            var historyRequest = new HistoryRequest
            {                
                Id = History.Id,                
                Description = History.Description,
                MotorcycleId = MotorCycle.Id,                
                InicialDate = History.InicialDate,
                FinalDate = History.FinalDate,
                Remarks = MotorCycle.Remarks,
                WorkshopType = History.WorkshopType                
            };

            Response response;
            if (IsEdit)
            {
                response = await _apiService.PutAsync(
                    url, "/api", "/Workshops", historyRequest.Id, historyRequest, "bearer", token.Token);
            }
            else
            {
                response = await _apiService.PostAsync(
                    url, "/api", "/Workshops", historyRequest, "bearer", token.Token);
            }

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Ok,
                string.Format(Languages.CreateEditHistoryConfirm, IsEdit ? Languages.Edited : Languages.Created),
                Languages.Accept);

            //await MotorCyclesPageViewModel.GetInstance().UpdateOwnerAsync();
            await _navigationService.GoBackToRootAsync();
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(History.Description))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, Languages.HistoryDescriptionError, Languages.Accept);
                return false;
            }

            if (History.FinalDate < History.InicialDate)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, Languages.FinalDateError, Languages.Accept);
                return false;
            }

            if (WorkshopType == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, Languages.WorkshopTypeError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
