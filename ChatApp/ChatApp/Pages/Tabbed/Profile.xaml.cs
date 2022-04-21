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
            Application.Current.MainPage = new MainPage();
        }
    }
}