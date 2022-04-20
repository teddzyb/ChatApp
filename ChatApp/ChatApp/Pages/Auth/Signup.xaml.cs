using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
                await DisplayAlert("Error", "Missing Fields", "", "Okay");
                return;
            }

            // Code here...
        }
    }
}