using System;
using System.Collections.Generic;
using WiringDiagramConstructionMachine.Settings;
using WiringDiagramConstructionMachine.Views;
using Xamarin.Forms;

namespace WiringDiagramConstructionMachine
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Init();
        }

        public async void Init()
        {
            if (UserLogged.IsLogged && UserLogged.Role == 0)// admin
            {
                FontImageSource IconUsers = new FontImageSource();
                IconUsers.FontFamily = "FontAwesomeSolid";
                IconUsers.Size = 16;
                IconUsers.Glyph = "\uf0c0";
                ShellContent shellContentUsers = new ShellContent();
                shellContentUsers.ContentTemplate = new DataTemplate(typeof(UsersPage));

                Tab tabUsers = new Tab()
                {
                    Title = "Users",
                    Icon = IconUsers,
                };
                tabUsers.Items.Add(shellContentUsers);
                tabBarMenu.Items.Add(tabUsers);
            }

            FontImageSource fontImageSource = new FontImageSource();
            fontImageSource.FontFamily = "FontAwesomeSolid";
            fontImageSource.Size = 16;
            fontImageSource.Glyph = "\uf2f5";
            ShellContent shellContentDangXuat = new ShellContent();
            shellContentDangXuat.ContentTemplate = new DataTemplate(typeof(LogOutPage));
            
            Tab tabDangXuat = new Tab()
            {
                Title= "Đăng xuất",
                Icon = fontImageSource,
            };
            tabDangXuat.Items.Add(shellContentDangXuat);
            tabBarMenu.Items.Add(tabDangXuat);
        }
    }
}
