using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Common.Services;
using HelpMoto.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Prism.ViewModels
{
    public class MotorCyclesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private OwnerResponse _owner;
        private ObservableCollection<MotorCycleItemViewModel> _motorCycles;
        private DelegateCommand _addMotorCycleCommand;
        private static MotorCyclesPageViewModel _instance;

        public MotorCyclesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.MyMotorCycles;
            LoadOwner();
        }

        public DelegateCommand AddMotorCycleCommand => _addMotorCycleCommand ?? (_addMotorCycleCommand = new DelegateCommand(AddMotorCycle));

        public ObservableCollection<MotorCycleItemViewModel> MotorCycles
        {
            get => _motorCycles;
            set => SetProperty(ref _motorCycles, value);
        }

        public static MotorCyclesPageViewModel GetInstance()
        {
            return _instance;
        }

        public async Task UpdateOwnerAsync()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.GetOwnerByEmailAsync(
                url,
                "/api",
                "/Owners/GetOwnerByEmail",
                "bearer",
                token.Token,
                _owner.Email);

            if (response.IsSuccess)
            {
                var owner = (OwnerResponse)response.Result;
                Settings.Owner = JsonConvert.SerializeObject(owner);
                _owner = owner;
                LoadOwner();
            }
        }

        private void LoadOwner()
        {
            _owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);
            MotorCycles = new ObservableCollection<MotorCycleItemViewModel>(_owner.Motorcycles.Select(p => new MotorCycleItemViewModel(_navigationService)
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,                
                Brand = p.Brand,
                Shop = p.Shop,                
                Remarks = p.Remarks,
                ShopLocal = p.ShopLocal,
                MotorcycleType = p.MotorcycleType,
                Histories = p.Histories
            }).ToList());
        }

        private async void AddMotorCycle()
        {
            await _navigationService.NavigateAsync("EditMotorCyclePage");
        }
    }
}
