using System;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace WiringDiagramConstructionMachine.Settings
{
    public class UserLogged
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static Guid Id
        {
            get => AppSettings.GetValueOrDefault(nameof(Id), Guid.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Id), value);
        }

        public static string Phone
        {
            get => AppSettings.GetValueOrDefault(nameof(Phone), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Phone), value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(nameof(AccessToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(AccessToken), value);
        }

        public static bool IsLogged
        {
            //get => Id != Guid.Empty && AccessToken != string.Empty;
            get => AppSettings.GetValueOrDefault(nameof(IsLogged), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsLogged), value);
        }

        public static int Role
        {
            get => AppSettings.GetValueOrDefault(nameof(Role), -1);
            set => AppSettings.AddOrUpdateValue(nameof(Role), value);
        }

        public static bool IsPaid
        {
            get => AppSettings.GetValueOrDefault(nameof(IsPaid), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsPaid), value);
        }

        public static DateTime CreatedDate
        {
            get => AppSettings.GetValueOrDefault(nameof(CreatedDate), DateTime.Now);
            set => AppSettings.AddOrUpdateValue(nameof(CreatedDate), value);
        }

        public static async Task Logout()
        {
            Id = Guid.Empty;
            Phone = string.Empty;
            Password = string.Empty;
            AccessToken = string.Empty;
            IsLogged = false;
            Role = -1;
            IsPaid = false;
            CreatedDate = DateTime.Now;
        }
    }
}
