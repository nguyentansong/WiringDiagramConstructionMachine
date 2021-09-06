using System;
using System.Collections.Generic;
using WiringDiagramConstructionMachine.Models;
using WiringDiagramConstructionMachine.ViewModels;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class UsersPage : ContentPage
    {
        public static bool NeedToRefresh;
        public UsersPageViewModel viewModel;
        public UsersPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new UsersPageViewModel();
            NeedToRefresh = false;
            Init();
        }

        public async void Init()
        {
            Loading.Show();
            await viewModel.LoadUsers();
            Loading.Hide();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (NeedToRefresh)
            {
                await viewModel.RefreshData();
                NeedToRefresh = false;
            }
        }

        private async void SearchBar_SearchButtonPressed(System.Object sender, System.EventArgs e)
        {
            Loading.Show();
            viewModel.Users.Clear();
            await viewModel.RefreshData();
            Loading.Hide();
        }

        private void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Keyword))
            {
                SearchBar_SearchButtonPressed(null, EventArgs.Empty);
            }
        }

        private async void listView_ItemAppearing(System.Object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if (!viewModel.OutOfData && viewModel.Users != null && e.Item == viewModel.Users[viewModel.Users.Count - 1])
            {
                viewModel.Page++;
                await viewModel.LoadUsers();
            }
        }

        private async void ListViewItem_Tapped(object sender, ItemTappedEventArgs e)
        {
            Loading.Show();
            var item = e.Item as UserModel;

            await Navigation.PushAsync(new UserUpdatePage(item.Id));
            Loading.Hide();
        }
    }
}
