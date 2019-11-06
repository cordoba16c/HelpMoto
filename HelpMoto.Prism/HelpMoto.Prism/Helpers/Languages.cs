using HelpMoto.Prism.Interfaces;
using HelpMoto.Prism.Resources;
using Xamarin.Forms;

namespace HelpMoto.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string Login => Resource.Login;

        public static string LoginError => Resource.LoginError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Rememberme => Resource.Rememberme;

        public static string Loading => Resource.Loading;

        public static string MyMotorCycles => Resource.MyMotorCycles;

        public static string ModifyProfile => Resource.ModifyProfile;

        public static string Logout => Resource.Logout;

        public static string Profile => Resource.Profile;

        public static string CheckConnection => Resource.CheckConnection;
    }
}
