using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WiringDiagramConstructionMachine.Settings;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class LogOutPage : ContentPage
    {
        public LogOutPage()
        {
            InitializeComponent();
            Loading.IsVisible = true;
            this.BackgroundColor = Color.Red;
            UserLogged.AccessToken = string.Empty;
            UserLogged.Id = Guid.Empty;
            UserLogged.IsLogged = false;
            UserLogged.Password = string.Empty;
            UserLogged.Phone = string.Empty;
            UserLogged.Role = -1;
            UserLogged.IsPaid = false;
            UserLogged.CreatedDate = DateTime.Now;
            App.Current.MainPage = new LoginPage();
            Loading.IsVisible = false;
        }
    }
}
