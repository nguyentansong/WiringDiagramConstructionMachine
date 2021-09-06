using System;
using System.Threading.Tasks;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Helper;
using WiringDiagramConstructionMachine.Models;

namespace WiringDiagramConstructionMachine.ViewModels
{
    public class UserUpdatePageViewModel :BaseViewModel
    {
        private UserModel _user;
        public UserModel User { get => _user; set { _user = value;OnPropertyChanged(nameof(User)); } }

        public Guid UserId;

        public UserUpdatePageViewModel()
        {
        }

        public async Task LoadUser()
        {
            ApiResponse apiResponse = await ApiHelper.Get<UserModel>($"{Configuration.ApiRouter.GET_USER}?userId={UserId}",true);
            if (apiResponse.IsSuccess)
            {
                this.User = apiResponse.Content as UserModel;
            }
        }

        public async Task<bool> UpdateUser()
        {
            ApiResponse apiResponse = await ApiHelper.Put($"{Configuration.ApiRouter.UPDATE_USER}", User, true);
            if (apiResponse.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
