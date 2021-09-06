using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Configuration;
using WiringDiagramConstructionMachine.Controls;
using WiringDiagramConstructionMachine.Helper;
using WiringDiagramConstructionMachine.IServices;
using WiringDiagramConstructionMachine.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class LoginPage : ContentPage
    {
        private string _phone;
        public string Phone { get => _phone;set { _phone = value; OnPropertyChanged(nameof(Phone)) ; } }

        private string _password;
        public string Password { get => _password;set { _password = value; OnPropertyChanged(nameof(Password)); } }

        private bool _isLogin;
        public bool IsLogin { get => _isLogin; set { _isLogin = value; OnPropertyChanged(nameof(IsLogin)); } }

        private bool _isRegister;
        public bool IsRegister { get => _isRegister; set { _isRegister = value; OnPropertyChanged(nameof(IsRegister)); } }

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            IsLogin = true;
            VisualStateManager.GoToState(frDangNhap, "Active");
            VisualStateManager.GoToState(lblDangNhap, "Active");
            VisualStateManager.GoToState(frDangKy, "InActive");
            VisualStateManager.GoToState(lblDangKy, "InActive");
            Loading.Hide();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async()=> {
                var asw = await DisplayAlert("Thông báo!", "Bạn có muốn thoát khỏi ứng dụng?", "Đồng ý", "Huỹ");
                if (asw)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }

        private void DangNhap_Tapped(object sender, EventArgs e)
        {
            IsLogin = true;
            IsRegister = false;
            VisualStateManager.GoToState(frDangNhap, "Active");
            VisualStateManager.GoToState(lblDangNhap, "Active");
            VisualStateManager.GoToState(frDangKy, "InActive");
            VisualStateManager.GoToState(lblDangKy, "InActive");
        }

        private void DangKy_Tapped(object sender, EventArgs e)
        {
            IsLogin = false;
            IsRegister = true;
            VisualStateManager.GoToState(frDangNhap, "InActive");
            VisualStateManager.GoToState(lblDangNhap, "InActive");
            VisualStateManager.GoToState(frDangKy, "Active");
            VisualStateManager.GoToState(lblDangKy, "Active");
        }

        public async void Login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Phone))
            {
                await DisplayAlert("", "Vui lòng nhập số điện thoại", "Ok");
                return;
            }

            if (!ValidatePhone.InValidatePhone(Phone))
            {
                await DisplayAlert("", "Số điện thoại không đúng định dạng. Vui lòng thử lại", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                await DisplayAlert("", "Vui lòng nhập mật khẩu", "Ok");
                return;
            }

            Loading.Show();
            string serial = await DependencyService.Get<IGetSerialDeviceService>().GetSerial();

            ApiResponse apiResponse = await ApiHelper.Login(Phone, Password, serial);
            if (apiResponse.IsSuccess && apiResponse.Content != null)
            {
                AuthenticateReponse auth = JsonConvert.DeserializeObject<AuthenticateReponse>(apiResponse.Content.ToString());
                UserLogged.IsLogged = true;
                UserLogged.AccessToken = auth.AccessToken;
                UserLogged.Id = auth.Id;
                UserLogged.Phone = auth.Phone;
                UserLogged.Password = auth.Password;
                if (auth.IsPaid.HasValue)
                {
                    UserLogged.IsPaid = auth.IsPaid.Value;
                }

                if (auth.CreateDate.HasValue)
                {
                    UserLogged.CreatedDate = auth.CreateDate.Value;
                }
                
                if (auth.Role.HasValue)
                {
                    UserLogged.Role = auth.Role.Value;
                }

                Xamarin.Forms.Application.Current.MainPage  = new AppShell();
                Loading.Hide();
            }
            else
            {
                await DisplayAlert("", apiResponse.Message, "Ok");
                Loading.Hide();
            }
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Phone))
            {
                await DisplayAlert("", "Vui lòng nhập số điện thoại", "Ok");
                return;
            }

            if (!ValidatePhone.InValidatePhone(Phone))
            {
                await DisplayAlert("", "Số điện thoại không đúng định dạng. Vui lòng thử lại", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                await DisplayAlert("", "Vui lòng nhập mật khẩu", "Ok");
                return;
            }

            Loading.Show();

            string serial = await DependencyService.Get<IGetSerialDeviceService>().GetSerial();

            ApiResponse apiResponse = await ApiHelper.Post(ApiRouter.REGISTER, new { Phone = Phone, Password = Password,SerialDevice=serial, IsPaid= true });
            if (apiResponse.IsSuccess && apiResponse.Content != null)
            {
                AuthenticateReponse auth = JsonConvert.DeserializeObject<AuthenticateReponse>(apiResponse.Content.ToString());
                UserLogged.IsLogged = true;
                UserLogged.AccessToken = auth.AccessToken;
                UserLogged.Id = auth.Id;
                UserLogged.Phone = auth.Phone;
                UserLogged.Password = auth.Password;
                if (auth.IsPaid.HasValue)
                {
                    UserLogged.IsPaid = auth.IsPaid.Value;
                }
                if (auth.CreateDate.HasValue)
                {
                    UserLogged.CreatedDate = auth.CreateDate.Value;
                }
                
                if (auth.Role.HasValue)
                {
                    UserLogged.Role = auth.Role.Value;
                }

                Xamarin.Forms.Application.Current.MainPage = new AppShell();
                Loading.Hide();
            }
            else
            {
                await DisplayAlert("", apiResponse.Message, "Ok");
                Loading.Hide();
            }
        }
    }
}