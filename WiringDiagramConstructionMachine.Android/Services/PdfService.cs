using System;
using System.Threading.Tasks;
using Android.Content;
using WiringDiagramConstructionMachine.Droid.Services;
using WiringDiagramConstructionMachine.IServices;

[assembly: Xamarin.Forms.Dependency(typeof(PdfService))]
namespace WiringDiagramConstructionMachine.Droid.Services
{
    public class PdfService :IPdfService
    {
        public async Task View(string url, string name)
        {
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(Android.Net.Uri.Parse(url), "application/pdf");
            intent.SetFlags(ActivityFlags.NoUserAction | ActivityFlags.NoHistory );
            try
            {
                //Android.App.Application.Context.StartActivity(intent);
                Xamarin.Essentials.Platform.CurrentActivity.StartActivity(intent);
            }
            catch (Exception ex)
            {
                //await AppShell.Current.Navigation.PushAsync(new WiringDiagramConstructionMachine.Views.PdfViewPage(url) { Title = name });
            }
        }
    }
}
