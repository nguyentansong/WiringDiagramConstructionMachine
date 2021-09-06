using System;
using Android.Content;
using WiringDiagramConstructionMachine.Controls;
using WiringDiagramConstructionMachine.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(testWebView),typeof(testWebViewRenderer))]
namespace WiringDiagramConstructionMachine.Droid.Renderers
{
    public class testWebViewRenderer: WebViewRenderer
    {
        public testWebViewRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as testWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                if (!customWebView.IsPdf)
                    Control.LoadUrl(customWebView.Uri);
                else
                    Control.LoadUrl("https://docs.google.com/gview?embedded=true&url=" + customWebView.Uri);
            }
        }
    }
}
