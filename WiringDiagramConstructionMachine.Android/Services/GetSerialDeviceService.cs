using System;
using System.Threading.Tasks;
using WiringDiagramConstructionMachine.Droid.Services;
using WiringDiagramConstructionMachine.IServices;

[assembly: Xamarin.Forms.Dependency(typeof(GetSerialDeviceService))]
namespace WiringDiagramConstructionMachine.Droid.Services
{
    public class GetSerialDeviceService : IGetSerialDeviceService
    {
        public async Task<string> GetSerial()
        {
            return Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }
    }
}
