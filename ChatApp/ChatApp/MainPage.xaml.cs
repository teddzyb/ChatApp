using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using ChatApp.Pages.Auth;
using ChatApp.Pages.Tabbed;

namespace ChatApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
 
        private async void Btn_SignIn(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);

            if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                if (string.IsNullOrEmpty(emailEntry.Text))
                {
                    emailFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    emailEntry.Focus();
                }
                
                if (string.IsNullOrEmpty(passwordEntry.Text))
                {
                    passwordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    if (!string.IsNullOrEmpty(emailEntry.Text))
                    {
                        passwordEntry.Focus();
                    }
                    
                }
                await DisplayAlert("Error", "Missing Fields", "", "OKAY");
                return;
            }

            if (true) // Email is not verified
            {
                await DisplayAlert("Error", "Email is not verified. A new verification link has been sent.", "", "OKAY");
                return;
            }
            
            //Application.Current.MainPage = new MainTabbed();
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
