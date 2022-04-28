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
        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //UserData users = new UserData();
            //userList = UserData.userList; 

            userList.Add(new UserModel() { id = "1", username = "User Test-1", email = "utest1@gmail.com", password = "1234", contacts = new List<string>(new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10" }), isVerified = true });
            userList.Add(new UserModel() { id = "2", username = "User Test-2", email = "utest2@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = false });
            userList.Add(new UserModel() { id = "3", username = "User Test-3", email = "utest3@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "4", username = "User Test-4", email = "utest4@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "5", username = "User Test-5", email = "utest5@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "6", username = "User Test-6", email = "utest6@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "7", username = "User Test-7", email = "utest7@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "8", username = "User Test-8", email = "utest8@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "9", username = "User Test-9", email = "utest9@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "10", username = "User Test-10", email = "utest10@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });

            UserData.userList = userList;

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

            // Query (Insert Future Code Here..)

            if(!userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text).Any())
            {
                await DisplayAlert("Error", "Invalid Email or Password", "", "OKAY");
                return;
            }

            // Email is not verified
            if (userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text && x.isVerified == false).Any())
            {
                await DisplayAlert("Error", "Email is not verified. A new verification link has been sent.", "", "OKAY");
                return;
            }

            // Successful authentication with database
            var user = userList.Where(x => x.email == EmailEntry.Text && x.password == PasswordEntry.Text).FirstOrDefault();

            Application.Current.Properties["id"] = user.id;
            Application.Current.Properties["username"] = user.username;
            Application.Current.Properties["email"] = EmailEntry.Text;
            await Application.Current.SavePropertiesAsync();

            var MainTabbed = new MainTabbed();
            MainTabbed.BindingContext = new UserModel { username = user.username, email = EmailEntry.Text };

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
