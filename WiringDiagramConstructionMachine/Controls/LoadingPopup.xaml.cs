using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WiringDiagramConstructionMachine.Controls
{
    public partial class LoadingPopup : ContentView
    {
        private bool _showHide;
        public bool ShowHide { get =>_showHide; set { _showHide = value;OnPropertyChanged(nameof(ShowHide)); } }

        public LoadingPopup()
        {
            InitializeComponent();
            this.BindingContext = this;
            this.ShowHide = true;
        }

        public void Show()
        {
            this.ShowHide = true;
        }

        public void Hide()
        {
            this.ShowHide = false;
        }
    }
}
