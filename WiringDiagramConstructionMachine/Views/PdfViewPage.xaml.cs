using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WiringDiagramConstructionMachine.IServices;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Views
{
    public partial class PdfViewPage : ContentPage
    {
        private string _url;
        public PdfViewPage(string url)
        {
            InitializeComponent();
            _url = url;
            Init();
        }

        public async void Init()
        {
            var localPath = string.Empty;

            if (Device.RuntimePlatform == Device.Android)
            {
                var dependency = DependencyService.Get<ILocalFileService>();

                if (dependency == null)
                {
                    await DisplayAlert("Error loading PDF", "Computer says no", "OK");
                    return;
                }

                var fileName = Guid.NewGuid().ToString();

                // Download PDF locally for viewing
                using (var httpClient = new HttpClient())
                {
                    var pdfStream = Task.Run(() => httpClient.GetStreamAsync(_url)).Result;

                    localPath =
                        Task.Run(() => dependency.SaveFileToDisk(pdfStream, $"{fileName}.pdf")).Result;
                }

                if (string.IsNullOrWhiteSpace(localPath))
                {
                    await DisplayAlert("Error loading PDF", "Computer says no", "OK");

                    return;
                }
            }

            //if (Device.RuntimePlatform == Device.Android)
            //    PdfDocView.Source = $"file:///android_asset/pdfjs/web/viewer.html?file={"file:///" + WebUtility.UrlEncode(localPath)}";
            //else
            //    PdfDocView.Source = _url;
        }
    }
}
