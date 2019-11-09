using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace HelpMoto.Common.Helpers
{
    public static class Settings
    {
        private const string _motorCycle = "MotorCycle";
        private const string _token = "Token";
        private const string _owner = "Owner";
        private const string _isRemembered = "IsRemembered";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string MotorCycle
        {
            get => AppSettings.GetValueOrDefault(_motorCycle, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_motorCycle, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Owner
        {
            get => AppSettings.GetValueOrDefault(_owner, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_owner, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
    }
}
