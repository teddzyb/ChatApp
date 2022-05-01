using System;
using Xamarin.Forms;
using ChatApp.Pages.Auth;
using ChatApp.Pages.Tabbed;
using ChatApp.TempData;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

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
            EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);

            if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                if (string.IsNullOrEmpty(EmailEntry.Text))
                {
                    EmailFrame.BorderColor = Color.FromRgb(244, 67, 54);
                }
                
                if (string.IsNullOrEmpty(PasswordEntry.Text))
                {
                    passwordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    
                }
                await DisplayAlert("Error", "Missing Fields", "", "OKAY");
                return;
            }

            if (!ValidateEmail.IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Error", "The email address is badly formatted.", "", "OKAY");
                return;
            }

            // Query (Insert Future Code Here..)
            
            if(!GlobalData.userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text).Any())
            {
                await DisplayAlert("Error", "There is no user record corresponding to this identifier. The user may have been deleted.", "", "OKAY");
                return;
            }

            // Email is not verified
            if (GlobalData.userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text && x.isVerified == false).Any())
            {
                await DisplayAlert("Error", "Email is not verified. A new verification link has been sent.", "", "OKAY");
                return;
            }

            // Successful authentication with database
            var user = GlobalData.userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text).FirstOrDefault();

            Application.Current.Properties["id"] = user.uid;
            Application.Current.Properties["username"] = user.username;
            Application.Current.Properties["email"] = EmailEntry.Text;
            await Application.Current.SavePropertiesAsync();

            var MainTabbed = new MainTabbed();
            MainTabbed.BindingContext = new UserModel { username = user.username, email = EmailEntry.Text };

            Application.Current.MainPage = new NavigationPage(MainTabbed);
        }

        private async void Btn_SignUp(object sender, EventArgs e)
        {
            //EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            //passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
            
            await Navigation.PushAsync(new Signup(), true);
        }

        private async void Btn_ResetPass(object sender, EventArgs e)
        {
            //EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            //passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);

            await Navigation.PushAsync(new ResetPass(), true);
        }

        private void Focused_Email(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Focused_Password(object sender, EventArgs e)
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
