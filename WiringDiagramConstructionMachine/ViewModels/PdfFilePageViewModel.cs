using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Helper;
using WiringDiagramConstructionMachine.Settings;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.ViewModels
{
    public class PdfFilePageViewModel:BaseViewModel
    {
        public ObservableCollection<PdfFiles> PdfFiles { get; set; } = new ObservableCollection<PdfFiles>();

        public int Permission = UserLogged.Role;
        public int sunMenuParentId { get; set; }

        public int Page = 1;
        public bool OutOfData;

        private bool _isRefreshing = false;
        public bool IsRefreshing { get => _isRefreshing; set { _isRefreshing = value;OnPropertyChanged(nameof(IsRefreshing)); } }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    await RefreshData();
                    IsRefreshing = false;
                });
            }
        }

        public PdfFilePageViewModel()
        {
        }

        public async Task LoadListPdf()
        {
            ApiResponse apiResponse = await ApiHelper.Get<List<PdfFiles>>($"{Configuration.ApiRouter.GET_LIST_PDF}?page={Page}&parentid={sunMenuParentId}",true);
            if (apiResponse.IsSuccess && apiResponse.Content != null)
            {
                var data = apiResponse.Content as List<PdfFiles>;
                if (data.Count == 0)
                {
                    OutOfData = true;
                }
                foreach (var item in data)
                {
                    PdfFiles.Add(item);
                }
            }
        }

        public async Task<ApiResponse> DeletePdf(Guid id)
        {
            return await ApiHelper.Delete($"{Configuration.ApiRouter.DELETE_PDF}?id={id}");
        }

        public async Task RefreshData()
        {
            Page = 1;
            PdfFiles.Clear();
            await LoadListPdf();
        }
    }
}
