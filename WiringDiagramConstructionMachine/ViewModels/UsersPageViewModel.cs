using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Helper;
using WiringDiagramConstructionMachine.Models;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.ViewModels
{
    public class UsersPageViewModel :BaseViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; } = new ObservableCollection<UserModel>();

        private string _keyword;
        public string Keyword { get => _keyword; set { _keyword = value;OnPropertyChanged(nameof(Keyword)); } }

        private bool _isRefreshing = false;
        public bool IsRefreshing { get => _isRefreshing; set { _isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); } }

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

        public int Page = 1;
        public bool OutOfData;

        public UsersPageViewModel()
        {
        }

        public async Task LoadUsers()
        {
            ApiResponse apiResponse = await ApiHelper.Get<List<UserModel>>($"{Configuration.ApiRouter.GET_USERS}?page={Page}&keyword={Keyword}",true);
            if (apiResponse.Content != null && apiResponse.IsSuccess)
            {
                List<UserModel> users = apiResponse.Content as List<UserModel>;
                if (users.Count == 0)
                {
                    OutOfData = true;
                }
                foreach (var item in users)
                {
                    Users.Add(item);
                }
            }
        }
        public async Task RefreshData()
        {
            Page = 1;
            Users.Clear();
            await LoadUsers();
        }
    }
}
