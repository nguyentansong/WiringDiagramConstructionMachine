using System;
using System.Collections.Generic;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Helper;
using WiringDiagramConstructionMachine.IServices;
using WiringDiagramConstructionMachine.Models;
using WiringDiagramConstructionMachine.Settings;
using WiringDiagramConstructionMachine.ViewModels;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class PdfListPage : ContentPage
    {
        public Action<bool> OnCompleted;
        public PdfFilePageViewModel viewModel;

        public PdfListPage(int subMenuParentId)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new PdfFilePageViewModel();
            viewModel.sunMenuParentId = subMenuParentId;
            Init();
        }
        public async void Init()
        {
            await viewModel.LoadListPdf();
            if (viewModel.PdfFiles != null)
            {
                OnCompleted?.Invoke(true);
            }
            else
            {
                OnCompleted?.Invoke(false);
            }
            Loading.Hide();
        }

        private async void listView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Loading.Show();
            var item = e.Item as PdfFiles;
            FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
            string urlFile = await firebaseStorageHelper.GetFile(item.PdfFile);
            //await Xamarin.Essentials.Browser.OpenAsync("https://docs.google.com/gview?embedded=true&url=" + urlFile);

            await DependencyService.Get<IPdfService>().View(urlFile, item.Name);

            //string[] options = new string[] { "Xem file", "Cập nhật" };
            //string asw = await DisplayActionSheet("Tuỳ chọn", "Đóng", null, options);
            //if (asw == "Xem file")
            //{
            //    FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
            //    string urlFile = await firebaseStorageHelper.GetFile(item.PdfFile);
            //    await DependencyService.Get<IPdfService>().View(urlFile, item.Name);
            //}
            //else if (asw == "Cập nhật")
            //{
            //    AddFilePdfPage addFilePdfPage = new AddFilePdfPage(item.Id);
            //    addFilePdfPage.OnCompleted = async() =>
            //    {
            //        await Navigation.PushAsync(addFilePdfPage);
            //    };
            //}
            Loading.Hide();
        }

        private async void Deleted_Tapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Thông báo", "Bạn có chắc chắn muốn xoá file này?", "Đồng ý", "Huỹ");
            if (answer)
            {
                Loading.Show();
                var item = (PdfFiles)((sender as Label).GestureRecognizers[0] as TapGestureRecognizer).CommandParameter;

                ApiResponse apiResponse =  await viewModel.DeletePdf(item.Id);
                if (apiResponse.IsSuccess)
                {
                    try
                    {
                        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
                        await firebaseStorageHelper.DeleteFile(item.PdfFile);
                        await viewModel.RefreshData();
                        Loading.Hide();
                        await DisplayAlert("", "Thành công", "Đóng");
                    }
                    catch(Exception ex)
                    {
                        Loading.Hide();
                        await DisplayAlert("", ex.Message, "Đóng");
                    }
                }
                else
                {
                    Loading.Hide();
                    await DisplayAlert("",apiResponse.Message, "Đóng");
                }
            }
        }

        private async void listView_ItemAppearing(System.Object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if (!viewModel.OutOfData && viewModel.PdfFiles != null && e.Item == viewModel.PdfFiles[viewModel.PdfFiles.Count - 1])
            {
                viewModel.Page++;
                await viewModel.LoadListPdf();
            }
        }
    }
}
