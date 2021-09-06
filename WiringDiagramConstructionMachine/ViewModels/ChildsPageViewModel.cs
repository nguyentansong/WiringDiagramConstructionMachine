using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WDCM_Api.Entities;
using WDCM_Api.Entities.Response;
using WiringDiagramConstructionMachine.Helper;

namespace WiringDiagramConstructionMachine.ViewModels
{
    public class ChildsPageViewModel:BaseViewModel
    {
        private List<Menu> _subMenus;
        public List<Menu> SubMenus { get => _subMenus; set { _subMenus = value; OnPropertyChanged(nameof(SubMenus)); } }

        public int parentId { get; set; }

        public ChildsPageViewModel()
        {
        }

        public async Task LoadSubMenu()
        {
            ApiResponse apiResponse = await ApiHelper.Get<List<Menu>>($"{Configuration.ApiRouter.GETSUBMENU}?id={parentId}");
            if (apiResponse.IsSuccess && apiResponse.Content != null)
            {
                this.SubMenus = apiResponse.Content as List<Menu>;
            }
        }
    }
}
