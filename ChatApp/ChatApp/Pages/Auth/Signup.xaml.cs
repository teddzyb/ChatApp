using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        public Signup()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
       
        private async void Btn_SignUp(object sender, EventArgs e)
        {
            bool isFocus = false;
            usernameFrame.BorderColor = Color.FromRgb(189, 189, 189);
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
            confirmPasswordFrame.BorderColor = Color.FromRgb(189, 189, 189);

            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text) || string.IsNullOrEmpty(confirmPasswordEntry.Text))
            {
                if (string.IsNullOrEmpty(usernameEntry.Text))
                {
                    usernameFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    if (!isFocus)
                    {
                        usernameEntry.Focus();
                        isFocus = true;
                    }
                }

                if (string.IsNullOrEmpty(emailEntry.Text))
                {
                    emailFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    if (!isFocus)
                    {
                        emailEntry.Focus();
                        isFocus = true;
                    }
                }

                if (string.IsNullOrEmpty(passwordEntry.Text))
                {
                    passwordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    if (!isFocus)
                    {
                        passwordEntry.Focus();
                        isFocus = true;
                    }
                }

                if (string.IsNullOrEmpty(confirmPasswordEntry.Text))
                {
                    confirmPasswordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    if (!isFocus)
                    {
                        confirmPasswordEntry.Focus();
                        isFocus = true;
                    }
                }
                
                await DisplayAlert("Error", "Missing Fields", "", "OKAY");
                return;
            }

            if (passwordEntry.Text != confirmPasswordEntry.Text)
            {
                passwordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                confirmPasswordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                passwordEntry.Focus();
                await DisplayAlert("Error", "Passwords do not match", "", "OKAY");
                return;
            }

            // Successful authentication with database (Insert Future Code Here..)
            if (true) 
            {
                await DisplayAlert("Error", "Sign up is successful. A verfication email has been sent.", "", "OKAY");
                return;
            }
        }

        private void TextChanged_Username(object sender, EventArgs e)
        {
            usernameFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void TextChanged_Email(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void TextChanged_Password(object sender, EventArgs e)
        {
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void TextChanged_ConfirmPassword(object sender, EventArgs e)
        {
            confirmPasswordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void Btn_SignUpWithGoogle(object sender, EventArgs e)
        {
            // Future Code Here...
        }

        private void Btn_SignUpWithFB(object sender, EventArgs e)
        {
            // Future Code Here...
        }
    }
}