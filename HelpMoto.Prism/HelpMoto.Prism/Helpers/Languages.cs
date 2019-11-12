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

        public static string OwnerError => Resource.OwnerError;

        public static string Ok => Resource.Ok;

        public static string UserUpdated => Resource.UserUpdated;

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;
        
        public static string LastNameError => Resource.LastNameError;

        public static string AddressError => Resource.AddressError;

        public static string MotorcycleType => Resource.MotorcycleType;

        public static string MotorcycleTypePlaceHolder => Resource.MotorcycleTypePlaceHolder;

        public static string Shop => Resource.Shop;

        public static string Brand => Resource.Brand;

        public static string BrandPlaceHolder => Resource.BrandPlaceHolder;

        public static string Cilinder => Resource.Cilinder;

        public static string CilinderPlaceHolder => Resource.CilinderPlaceHolder;

        public static string Delete => Resource.Delete;

        public static string Edited => Resource.Edited;

        public static string Created => Resource.Created;

        public static string CreateEditMotorcycleConfirm => Resource.CreateEditMotorcycleConfirm;

        public static string QuestionToDeleteMotorcycle => Resource.QuestionToDeleteMotorcycle;

        public static string Yes => Resource.Yes;

        public static string No => Resource.No;

        public static string MotorcycleTypeError => Resource.MotorcycleTypeError;

        public static string BrandError => Resource.BrandError;

        public static string CilinderError => Resource.CilinderError;

        public static string Confirm => Resource.Confirm;

        public static string ChangeImage => Resource.ChangeImage;

        public static string Remarks => Resource.Remarks;

        public static string MotorcycleGetError => Resource.MotorcycleGetError;

        public static string Cancel => Resource.Cancel;

        public static string QuestionToObtainImage => Resource.QuestionToObtainImage;

        public static string FromGallery => Resource.FromGallery;

        public static string FromCamera => Resource.FromCamera;

        public static string EditMotorcycle => Resource.EditMotorcycle;

        public static string NewMotorcycle => Resource.NewMotorcycle;

        public static string DetailMotorcycle => Resource.DetailMotorcycle;

        public static string InitialDate => Resource.InitialDate;

        public static string FinalDate => Resource.FinalDate;

        public static string WorkshopType => Resource.WorkshopType;

        public static string Description => Resource.Description;

        public static string History => Resource.History;
    }
}
