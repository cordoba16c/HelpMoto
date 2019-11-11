using Prism;
using Prism.Ioc;
using HelpMoto.Prism.ViewModels;
using HelpMoto.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HelpMoto.Common.Services;
using Newtonsoft.Json;
using HelpMoto.Common.Models;
using HelpMoto.Common.Helpers;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HelpMoto.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY2MTcxQDMxMzcyZTMzMmUzMGFtVitGKytWYnF6VkcxM1I0ejhEcm43Z2RlY0FDMFZaYjFIUTNabXljRUU9");
            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/HelpMotoMasterDetailPage/NavigationPage/MotorCyclesPage");
            }
            else 
            {
                await NavigationService.NavigateAsync("NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MotorCyclesPage, MotorCyclesPageViewModel>();
            containerRegistry.RegisterForNavigation<HelpMotoMasterDetailPage, HelpMotoMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MotorCyclePage, MotorCyclePageViewModel>();
            containerRegistry.RegisterForNavigation<EditMotorCyclePage, EditMotorCyclePageViewModel>();
        }
    }
}
