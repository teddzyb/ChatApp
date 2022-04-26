using System;
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

            // Email is not verified
            if (false) 
            {
                await DisplayAlert("Error", "Email is not verified. A new verification link has been sent.", "", "OKAY");
                return;
            }

            // Successful authentication with database (Insert Future Code Here..)
             Application.Current.MainPage = new NavigationPage(new MainTabbed());
        }

        private async void Btn_SignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup(), true);
        }

        private async void Btn_ResetPass(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPass(), true);
        }

        private void TextChanged_Email(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void TextChanged_Password(object sender, EventArgs e)
        {
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Btn_SignInWithGoogle(object sender, EventArgs e)
        {
            // Future Code Here...
        }

        private void Btn_SignInWithFB(object sender, EventArgs e)
        {
            // Future Code Here...
        }

    }
}
