using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentView
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Btn_SignOut(object sender, EventArgs e)
        {
            // (Insert code for sign out)
            ActivityIndicator.IsRunning = true;
            Application.Current.Properties.Clear();
            Application.Current.SavePropertiesAsync();

            ActivityIndicator.IsRunning = false;
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }
    }
}