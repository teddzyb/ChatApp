using System;
using Xamarin.Forms;
using ChatApp.Pages.Auth;
using ChatApp.Pages.Tabbed;
using ChatApp.TempData;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Plugin.CloudFirestore;

namespace ChatApp
{
    public partial class MainPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        [Obsolete]
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
            
            ActivityIndicator.IsRunning = true;
            
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<IFirebaseAuth>().LoginWithEmailPassword(EmailEntry.Text, PasswordEntry.Text);
            
            if (res.Status != true)
            {
                ActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", res.Response, "", "OKAY");
                return;
            }
            
            var MainTabbed = new MainTabbed();
            MainTabbed.BindingContext = new UserModel { username = dataClass.loggedInUser.username, email = dataClass.loggedInUser.email };
            Application.Current.MainPage = new NavigationPage(MainTabbed);

            ActivityIndicator.IsRunning = false;
        }

        private async void Btn_SignUp(object sender, EventArgs e)
        {
            //EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            //passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
            
            await Navigation.PushAsync(new Signup(), true);
        }

        private async void Btn_ResetPass(object sender, EventArgs e)
        {
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