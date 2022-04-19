using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using ChatApp.Views.Auth;
using ChatApp.Views.Tabbed;

namespace ChatApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void Btn_SignIn(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainTabbed();
        }

        private async void Btn_SignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup(), true);
        }

        private async void Btn_ResetPass(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPass(), true);
        }
    }
}
