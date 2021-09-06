using System;
using System.Collections.Generic;
using WiringDiagramConstructionMachine.Settings;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class KobelcoPage : ContentPage
    {
        public KobelcoPage()
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

        private void CrawlerExcavator_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            ChildsPage childsPage = new ChildsPage(4) { Title = "CRAWLER EXCAVATOR" };
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

        private void WhellExcavator_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            ChildsPage childsPage = new ChildsPage(5) { Title = "WHEEL EXCAVATOR" };
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
            AddFilePdfPage addFilePdfPage = new AddFilePdfPage(1) { Title = "Kobelco" };
            addFilePdfPage.OnCompleted = async () =>
            {
                await Navigation.PushAsync(addFilePdfPage);
                Loading.Hide();
            };
        }
    }
}
