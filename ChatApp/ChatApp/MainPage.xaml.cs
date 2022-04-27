using System;
using Xamarin.Forms;
using ChatApp.Pages.Auth;
using ChatApp.Pages.Tabbed;
using ChatApp.TempData;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatApp
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            UserData users = new UserData();
            userList = users.userList;
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
                    EmailEntry.Focus();
                }
                
                if (string.IsNullOrEmpty(PasswordEntry.Text))
                {
                    passwordFrame.BorderColor = Color.FromRgb(244, 67, 54);
                    if (!string.IsNullOrEmpty(EmailEntry.Text))
                    {
                        PasswordEntry.Focus();
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

            // Query (Insert Future Code Here..)

            if(!userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text).Any())
            {
                await DisplayAlert("Error", "Invalid Email or Password", "", "OKAY");
                return;
            }

            // Successful authentication with database
            string username = userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text).FirstOrDefault().username;
            
            Application.Current.Properties["username"] = username;
            Application.Current.Properties["email"] = EmailEntry.Text;
            await Application.Current.SavePropertiesAsync();

            var MainTabbed = new MainTabbed();
            MainTabbed.BindingContext = new UserModel { username = username, email = EmailEntry.Text };

            Application.Current.MainPage = new NavigationPage(MainTabbed);
        }

        private async void Btn_SignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup(), true);
            EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private async void Btn_ResetPass(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPass(), true);
            EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
            passwordFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }

        private void TextChanged_Email(object sender, EventArgs e)
        {
            EmailFrame.BorderColor = Color.FromRgb(189, 189, 189);
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
