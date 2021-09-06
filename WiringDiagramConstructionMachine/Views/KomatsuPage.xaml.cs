using System;
using System.Collections.Generic;
using WiringDiagramConstructionMachine.Settings;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class KomatsuPage : ContentPage
    {
        public KomatsuPage()
        {
            InitializeComponent();
            if (UserLogged.Role == 0)
            {
                stAdd.IsVisible = true;
            }
            Loading.Hide();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var asw = await DisplayAlert("Thông báo!", "Bạn có muốn thoát khỏi ứng dụng?", "Đồng ý", "Huỹ");
                if (asw)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }

        private void PcExcavator_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            ChildsPage childsPage = new ChildsPage(17) { Title = "PC EXCAVATOR" };
            childsPage.OnCompleted = async (IsSuccess) => {
                if (IsSuccess)
                {
                    await Navigation.PushAsync(childsPage);
                    Loading.Hide();
                }
                else
                {
                    Loading.Hide();
                    await DisplayAlert("", "Không tìm thấy thôn tin", "Đóng");
                }
            };
        }

        private void PwExcavator_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            ChildsPage childsPage = new ChildsPage(18) { Title = "PW EXCAVATOR" };
            childsPage.OnCompleted = async (IsSuccess) => {
                if (IsSuccess)
                {
                    await Navigation.PushAsync(childsPage);
                    Loading.Hide();
                }
                else
                {
                    Loading.Hide();
                    await DisplayAlert("", "Không tìm thấy thôn tin", "Đóng");
                }
            };
        }

        private void WhellLoader_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            ChildsPage childsPage = new ChildsPage(19) { Title = "WHEEL LOADER" };
            childsPage.OnCompleted = async (IsSuccess) => {
                if (IsSuccess)
                {
                    await Navigation.PushAsync(childsPage);
                    Loading.Hide();
                }
                else
                {
                    Loading.Hide();
                    await DisplayAlert("", "Không tìm thấy thôn tin", "Đóng");
                }
            };
        }

        private void DBulldozer_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            ChildsPage childsPage = new ChildsPage(20) { Title = "D BULLDOZER" };
            childsPage.OnCompleted = async (IsSuccess) => {
                if (IsSuccess)
                {
                    await Navigation.PushAsync(childsPage);
                    Loading.Hide();
                }
                else
                {
                    Loading.Hide();
                    await DisplayAlert("", "Không tìm thấy thôn tin", "Đóng");
                }
            };
        }

        private void AddFile_Clicked(object sender, EventArgs e)
        {
            Loading.Show();
            AddFilePdfPage addFilePdfPage = new AddFilePdfPage(2) { Title = "Komatsu" };
            addFilePdfPage.OnCompleted = async() =>
            {
                await Navigation.PushAsync(addFilePdfPage);
                Loading.Hide();
            };
        }
    }
}
