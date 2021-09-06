using System;
using System.Collections.Generic;
using WiringDiagramConstructionMachine.ViewModels;
using WDCM_Api.Entities;
using Xamarin.Forms;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Helper;
using Xamarin.Essentials;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class AddFilePdfPage : ContentPage
    {
        public Action OnCompleted;
        public AddFilePdfPageViewmodel viewmodel;
        private int _parentId;
        public Guid _pdfFileId;

        public AddFilePdfPage(int parentId)
        {
            InitializeComponent();
            this.BindingContext = viewmodel = new AddFilePdfPageViewmodel();
            _parentId = parentId;
            Init();
        }

        public AddFilePdfPage(Guid pdfFileId)
        {
            this.BindingContext = viewmodel = new AddFilePdfPageViewmodel();
            _pdfFileId = pdfFileId;
            Init();
        }

        public async void Init()
        {
            viewmodel.Parents = await viewmodel.LoadMenu(_parentId);
            if (_parentId == 3)//hitachi
            {
                viewmodel.IsHitachi = true;
            }
            OnCompleted?.Invoke();
            Loading.Hide();
        }

        public async void InitUpdate()
        {

        }

        private async void pickerParent_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            Loading.Show();
            var picker = sender as Picker;
            WDCM_Api.Entities.Menu item = picker.SelectedItem as WDCM_Api.Entities.Menu;
            viewmodel.Subs1 = null;
            viewmodel.Subs2 = null;
            viewmodel.Subs1 = await viewmodel.LoadMenu(item.Id);
            viewmodel.SelectedItemSub1 = null;
            viewmodel.SelectedItemSub2 = null;
            Loading.Hide();
        }

        private async void pickerSub1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (viewmodel.SelectedItemParent == null)
            {
                await DisplayAlert("", "Vui lòng chọn từ trên xuống", "Đóng");
                return;
            }
            Loading.Show();
            var picker = sender as Picker;
            WDCM_Api.Entities.Menu item = picker.SelectedItem as WDCM_Api.Entities.Menu;
            if (viewmodel.IsHitachi && item != null)
            {
                viewmodel.Subs2 = null;
                viewmodel.Subs2 = await viewmodel.LoadMenu(item.Id);
                viewmodel.SelectedItemSub2 = null;
            }
            Loading.Hide();
        }

        private async void pickerSub2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (viewmodel.SelectedItemSub1 == null)
            {
                await DisplayAlert("", "Vui lòng chọn từ trên xuống", "Đóng");
            }
        }

        private async void ChoseFile_Tapped(object sender, EventArgs e)
        {
            try
            {
                Loading.Show();
                var pickerFile = await FilePicker.PickAsync(new PickOptions()
                {
                    FileTypes = FilePickerFileType.Pdf,
                    PickerTitle = "Chọn File",
                });
                if (pickerFile != null)
                {
                    viewmodel.FileName = pickerFile.FileName;
                    viewmodel.Stream = await pickerFile.OpenReadAsync();

                    lblPdf.IsVisible = true;
                    Loading.Hide();
                }
                else
                {
                    Loading.Hide();
                    lblPdf.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (status == PermissionStatus.Denied)
                {
                    await DisplayAlert("", "Vui lòng cấp quyền để Wiring Diagram Construction Machine tiến hành up file.", "Ok");
                    this.ChoseFile_Tapped(null, EventArgs.Empty);
                }
                else
                {
                    await DisplayAlert("", "Lỗi hệ thống vui lòng thử lại.", "Đóng");
                }
            }

        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (viewmodel.SelectedItemParent == null)
            {
                await DisplayAlert("", "Vui lòng chọn danh mục", "Ok");
                return;
            }

            if (viewmodel.SelectedItemSub1 == null)
            {
                await DisplayAlert("", "Vui lòng chọn danh mục", "Ok");
                return;
            }

            if (viewmodel.IsHitachi && viewmodel.SelectedItemSub2 == null)
            {
                await DisplayAlert("", "Vui lòng chọn danh mục", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(viewmodel.Name))
            {
                await DisplayAlert("", "Vui lòng nhập tên file", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(viewmodel.FileName))
            {
                await DisplayAlert("", "Vui lòng chọn file", "Ok");
                return;
            }

            Loading.Show();

            FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
            var likeFile = await firebaseStorageHelper.UploadFile(viewmodel.Stream, viewmodel.FileName);
            if (string.IsNullOrWhiteSpace(likeFile))
            {
                await DisplayAlert("", "Lỗi up file. Vui lòng thử lại", "Ok");
                return;
            }

            viewmodel.PdfFiles.Id = Guid.NewGuid();
            viewmodel.PdfFiles.Name = viewmodel.Name;
            viewmodel.PdfFiles.PdfFile = viewmodel.FileName;
            viewmodel.PdfFiles.CreateDate = DateTime.Now;

            if (viewmodel.IsHitachi)
            {
                viewmodel.PdfFiles.ParentId = viewmodel.SelectedItemSub2.Id;
            }
            else
            {
                viewmodel.PdfFiles.ParentId = viewmodel.SelectedItemSub1.Id;
            }

            ApiResponse apiResponse = await ApiHelper.Post("api/pdffile", viewmodel.PdfFiles);
            if (apiResponse.IsSuccess)
            {
                await DisplayAlert("", "Thành công", "Ok");
                Loading.Hide();
                await Navigation.PopAsync();
            }
            else
            {
                Loading.Hide();
                await DisplayAlert("", "Lỗi hệ thống vui lòng thử lại", "Ok");
            }
        }
    }
}
