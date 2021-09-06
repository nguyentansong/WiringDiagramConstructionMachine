using System;
using System.Collections.Generic;
using WDCM_Api.Entities;
using WiringDiagramConstructionMachine.ViewModels;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class ChildsPage : ContentPage
    {
        public Action<bool> OnCompleted;
        public ChildsPageViewModel viewModel;

        public ChildsPage(int parentId)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new ChildsPageViewModel();
            viewModel.parentId = parentId;
            Init();
        }

        public async void Init()
        {
            await viewModel.LoadSubMenu();
            if (viewModel.SubMenus != null)
            {
                OnCompleted?.Invoke(true);
            }
            else
            {
                OnCompleted?.Invoke(false);
            }
            Loading.Hide();
        }

        private async void Item_Tapped(object sender, EventArgs e)
        {
            Loading.Show();
            var item= (WDCM_Api.Entities.Menu)((sender as Frame).GestureRecognizers[0] as TapGestureRecognizer).CommandParameter;
            if (item.IsHasSub)
            {
                ChildsPage childsPage = new ChildsPage(item.Id) { Title = item.Name };
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
            else
            {
                PdfListPage pdfListPage = new PdfListPage(item.Id) { Title = item.Name };
                pdfListPage.OnCompleted = async (IsSuccess) =>
                {
                    if (IsSuccess)
                    {
                        await Navigation.PushAsync(pdfListPage);
                        Loading.Hide();
                    }
                    else
                    {
                        Loading.Hide();
                        await DisplayAlert("", "Không tìm thấy thôn tin", "Đóng");
                    }
                };
                
            }
        }
    }
}
