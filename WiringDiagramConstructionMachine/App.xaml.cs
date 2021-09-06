using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WiringDiagramConstructionMachine.Views;
using WiringDiagramConstructionMachine.Settings;

namespace WiringDiagramConstructionMachine
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new BlankPage();
            //if (UserLogged.IsLogged == false)
            //{
            //    MainPage = new LoginPage();
            //}
            //else
            //{
            //    MainPage = new AppShell();
            //}

        }

        public async void CheckExpired()
        {
            TimeSpan expired = DateTime.Now - UserLogged.CreatedDate;
            int date = expired.Days;
            if (date >= 7)
            {
                await UserLogged.Logout();
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
            if (UserLogged.IsLogged && UserLogged.Role != 0)
            {
                CheckExpired();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
