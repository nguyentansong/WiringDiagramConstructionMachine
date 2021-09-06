using System;
using System.IO;
using System.Threading.Tasks;
using WiringDiagramConstructionMachine.Droid.Services;
using WiringDiagramConstructionMachine.IServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileService))]
namespace WiringDiagramConstructionMachine.Droid.Services
{
    public class LocalFileService :ILocalFileService
    {
        //private readonly string _rootDir = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "pdfjs");
        private readonly string _rootDir = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath, "pdfjs");
        public async Task<string> SaveFileToDisk(Stream pdfStream, string fileName)
        {
            if (!Directory.Exists(_rootDir))
                Directory.CreateDirectory(_rootDir);

            var filePath = Path.Combine(_rootDir, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await pdfStream.CopyToAsync(memoryStream);
                File.WriteAllBytes(filePath, memoryStream.ToArray());
            }

            return filePath;
        }
    }
}
