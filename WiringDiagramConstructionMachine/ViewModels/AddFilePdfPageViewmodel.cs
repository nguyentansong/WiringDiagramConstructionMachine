using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WDCM_Api.Entities.Response;
using WDCM_Api.Entities;
using WiringDiagramConstructionMachine.Helper;
using System.Collections.ObjectModel;
using System.IO;

namespace WiringDiagramConstructionMachine.ViewModels
{
    public class AddFilePdfPageViewmodel :BaseViewModel
    {
        private List<Menu> _parents;
        public List<Menu> Parents { get => _parents; set { _parents = value; OnPropertyChanged(nameof(Parents)); } }

        private List<Menu> _subs1;
        public List<Menu> Subs1 { get => _subs1; set { _subs1 = value;OnPropertyChanged(nameof(Subs1)); } }

        private List<Menu> _subs2;
        public List<Menu> Subs2 { get => _subs2; set { _subs2 = value; OnPropertyChanged(nameof(Subs2)); } }

        private bool _isHitachi;
        public bool IsHitachi { get => _isHitachi; set { _isHitachi = value; OnPropertyChanged(nameof(IsHitachi)); } }

        private Menu _selectedItemParent;
        public Menu SelectedItemParent { get => _selectedItemParent; set { _selectedItemParent = value; OnPropertyChanged(nameof(SelectedItemParent)); } }

        private Menu _selectedItemSub1;
        public Menu SelectedItemSub1 { get => _selectedItemSub1; set { _selectedItemSub1 = value;OnPropertyChanged(nameof(SelectedItemSub1)); } }

        private Menu _selectedItemSub2;
        public Menu SelectedItemSub2 { get => _selectedItemSub2; set { _selectedItemSub2 = value; OnPropertyChanged(nameof(SelectedItemSub2)); } }

        private string _name;
        public string Name { get => _name; set { _name = value;OnPropertyChanged(nameof(Name)); } }

        private string _fileName;
        public string FileName { get => _fileName; set { _fileName = value; OnPropertyChanged(nameof(FileName)); } }

        private PdfFiles _pdfFiles;
        public PdfFiles PdfFiles { get => _pdfFiles; set { _pdfFiles = value; OnPropertyChanged(nameof(PdfFiles)); } }

        public Stream Stream { get; set; }

        public AddFilePdfPageViewmodel()
        {
            PdfFiles = new PdfFiles();
        }

        public async Task<List<Menu>> LoadMenu(int parentId)
        {
            ApiResponse apiResponse = await ApiHelper.Get<List<Menu>>($"api/menu/parent?id={parentId}");
            if (apiResponse.IsSuccess && apiResponse.Content != null)
            {
                var data = apiResponse.Content as List<Menu>;
                return data;
            }
            else
            {
                return null;
            }
            
        }
    }
}
