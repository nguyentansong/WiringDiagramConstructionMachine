using System;
using System.Collections.Generic;
using WiringDiagramConstructionMachine.Settings;
using WiringDiagramConstructionMachine.ViewModels;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class UserUpdatePage : ContentPage
    {
        public Action<bool> OnCompleted;
        public UserUpdatePageViewModel viewModel;
        public UserUpdatePage()
        {
            InitializeComponent();
        }

        public UserUpdatePage(Guid UserId)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new UserUpdatePageViewModel();
            viewModel.UserId = UserId;
            Init();
        }

        public async void Init()
        {
            await viewModel.LoadUser();

            if (viewModel.User != null)
            {
                OnCompleted?.Invoke(true);
            }
            else
            {
                OnCompleted?.Invoke(false);
            }
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            if (UserLogged.Id == viewModel.User.Id)
            {
                await DisplayAlert("", "Không thể cập nhật. Đây là tài khoản của bạn", "Đóng");
                return;
            }

            bool isSuccess = await viewModel.UpdateUser();
            if (isSuccess)
            {
                await DisplayAlert("", "Cập nhật thành công", "Đóng");
                UsersPage.NeedToRefresh = true;
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("", "Cập nhật thất bại", "Đóng");
            }
        }
    }
}
