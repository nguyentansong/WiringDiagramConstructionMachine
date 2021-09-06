using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WiringDiagramConstructionMachine.IServices;
using WiringDiagramConstructionMachine.Models;
using WiringDiagramConstructionMachine.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine
{
    public partial class BlankPage : ContentPage
    {
        private readonly PdfDocEntity _pdfDocEntity;
        public BlankPage()
        {
            InitializeComponent();
            //Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
            //{
            //    Assembly assembly = typeof(BlankPage).Assembly;
            //    string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("https://firebasestorage.googleapis.com/v0/b/wdcm-db.appspot.com/o/WA800-1.pdf?alt=media&token=87212c26-9c5d-419f-915f-70721e7eb801"));
            //    Stream stream = assembly.GetManifestResourceStream(fileName);
            //    return stream;
            //});

            //Uri uri = new Uri("https://firebasestorage.googleapis.com/v0/b/wdcm-db.appspot.com/o/WA800-1.pdf?alt=media&token=87212c26-9c5d-419f-915f-70721e7eb801");

            //this.pdfViewer.Source = uri;

            //webView.Source = "https://docs.google.com/gview?embedded=true&url="+ "https://firebasestorage.googleapis.com/v0/b/wdcm-db.appspot.com/o/D150A-1%20D155A-1.pdf?alt=media&token=40cfa741-84aa-410a-87a8-b44c2357f4a1";
            //Xamarin.Essentials.Browser.OpenAsync("https://docs.google.com/gview?embedded=true&url=" + "https://firebasestorage.googleapis.com/v0/b/wdcm-db.appspot.com/o/D150A-1%20D155A-1.pdf?alt=media&token=40cfa741-84aa-410a-87a8-b44c2357f4a1");
            test.Uri = "https://firebasestorage.googleapis.com/v0/b/wdcm-db.appspot.com/o/WA800-1.pdf?alt=media&token=87212c26-9c5d-419f-915f-70721e7eb801";
        }

    }
}
