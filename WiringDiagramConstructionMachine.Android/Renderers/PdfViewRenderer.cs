using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Print;
using Android.Views;
using Android.Webkit;
using Android.Widget;
//using Com.Github.Barteksc.Pdfviewer;
using Java.IO;
using Java.Net;
using WiringDiagramConstructionMachine.Controls;
using WiringDiagramConstructionMachine.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;

[assembly: ExportRenderer(typeof(PdfView), typeof(PdfViewRenderer))]
namespace WiringDiagramConstructionMachine.Droid.Renderers
{
    public class PdfViewRenderer : ViewRenderer
    {
		public PdfViewRenderer(Context context) : base(context)
		{
		}

        //protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        //{
        //	base.OnElementChanged(e);

        //	if (e.NewElement != null)
        //	{
        //		Control.Settings.AllowFileAccess = true;
        //		Control.Settings.AllowFileAccessFromFileURLs = true;
        //		Control.Settings.AllowUniversalAccessFromFileURLs = true;
        //	}
        //}

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            Context context = Android.App.Application.Context;
            var view = (context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater).Inflate(Resource.Layout.PdfViewLayout, null);

            var web = view.FindViewById<Android.Webkit.WebView>(Resource.Id.webview);
            
            web.LoadUrl("https://firebasestorage.googleapis.com/v0/b/wdcm-db.appspot.com/o/D150A-1%20D155A-1.pdf?alt=media&token=40cfa741-84aa-410a-87a8-b44c2357f4a1");

            SetNativeControl(view);
            if (e.OldElement == null && e.NewElement != null)
            {
                
            }
        }
    }
}
