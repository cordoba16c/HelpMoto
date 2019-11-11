using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Common.Services;
using HelpMoto.Prism.Helpers;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelpMoto.Prism.ViewModels
{
    public class EditMotorCyclePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private MotorcycleResponse _motorCycle;
        private ImageSource _imageSource;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isEdit;
        private ObservableCollection<MotorcycleTypeResponse> _motorcycleTypes;
        private MotorcycleTypeResponse _motorcycleType;
        private MediaFile _file;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _deleteCommand;

        public EditMotorCyclePageViewModel(INavigationService navigationService,
                                           IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;            
        }

        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteAsync));

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public ObservableCollection<MotorcycleTypeResponse> MotorcycleTypes
        {
            get => _motorcycleTypes;
            set => SetProperty(ref _motorcycleTypes, value);
        }

        public MotorcycleTypeResponse MotorcycleType
        {
            get => _motorcycleType;
            set => SetProperty(ref _motorcycleType, value);
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

        public MotorcycleResponse MotorCycle
        {
            get => _motorCycle;
            set => SetProperty(ref _motorCycle, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("pet"))
            {
                MotorCycle = parameters.GetValue<MotorcycleResponse>("motorcycle");
                ImageSource = MotorCycle.ImageUrl;
                IsEdit = true;
                Title = Languages.EditMotorcycle;
            }
            else
            {
                MotorCycle = new MotorcycleResponse { Shop = DateTime.Today };
                ImageSource = "icon_no_image_add";
                IsEdit = false;
                Title = Languages.NewMotorcycle;
            }

            LoadMotorcycleTypesAsync();
        }

        private async void LoadMotorcycleTypesAsync()
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

            var response = await _apiService.GetListAsync<MotorcycleTypeResponse>(
                url,
                "/api",
                "/MotorcycleTypes",
                "bearer",
                token.Token);

            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MotorcycleGetError,
                    Languages.Accept);
                await _navigationService.GoBackAsync();
                return;
            }

            var motorcycleTypes = (List<MotorcycleTypeResponse>)response.Result;
            MotorcycleTypes = new ObservableCollection<MotorcycleTypeResponse>(motorcycleTypes);

            if (!string.IsNullOrEmpty(MotorCycle.MotorcycleType))
            {
                MotorcycleType = MotorcycleTypes.FirstOrDefault(mt => mt.Name == MotorCycle.MotorcycleType);
            }
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.QuestionToObtainImage,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == "Cancel")
            {
                _file = null;
                return;
            }

            if (source == "From camera")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
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
            var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = FilesHelper.ReadFully(_file.GetStream());
            }

            var motorcycleRequest = new MotorcycleRequest
            {
                Shop = MotorCycle.Shop,
                Id = MotorCycle.Id,
                ImageArray = imageArray,
                Name = MotorCycle.Name,
                OwnerId = owner.Id,
                MotorcycleTypeId = MotorcycleType.Id,
                Brand = MotorCycle.Brand, 
                Remarks = MotorCycle.Remarks
            };

            Response response;
            if (IsEdit)
            {
                response = await _apiService.PutAsync(
                    url, "/api", "/Motorcycle", motorcycleRequest.Id, motorcycleRequest, "bearer", token.Token);
            }
            else
            {
                response = await _apiService.PostAsync(
                    url, "/api", "/Motorcycle", motorcycleRequest, "bearer", token.Token);
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
                string.Format(Languages.CreateEditMotorcycleConfirm, IsEdit ? Languages.Edited : Languages.Created),
                Languages.Accept);

            await MotorCyclesPageViewModel.GetInstance().UpdateOwnerAsync();
            await _navigationService.GoBackToRootAsync();
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(MotorCycle.Name))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, Languages.CilinderError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(MotorCycle.Brand))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, Languages.BrandError, Languages.Accept);
                return false;
            }

            if (MotorcycleType == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error, Languages.MotorcycleTypeError, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void DeleteAsync()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.QuestionToDeleteMotorcycle,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await _apiService.DeleteAsync(
                url, "/api", "/Pets", MotorCycle.Id, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await MotorCyclesPageViewModel.GetInstance().UpdateOwnerAsync();

            IsRunning = false;
            IsEnabled = true;
            await _navigationService.GoBackToRootAsync();
        }
    }
}
