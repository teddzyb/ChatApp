using Plugin.CloudFirestore;
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
    public partial class MainTabbed : ContentPage
    {
        bool isFriendsNotExist = true;
        bool isLoaded = false;
        DataClass dataClass = DataClass.GetInstance;

        public MainTabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //MessagingCenter.Subscribe<MainPage>(this, "RefreshMainPage", (sender) =>
            //{
            //    isLoaded = false;
            //});

            checkUserContacts();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            isLoaded = false;
        }
        
        private void Nav_Chat(object sender, EventArgs e)
        {
            Chats.IsVisible = true;
            Profile.IsVisible = false;
            SearchBar.IsVisible = true;
            AlertLabel.IsVisible = isFriendsNotExist;

            ChatImage.Source = "chat_enabled.png";
            ChatLabel.TextColor = Color.FromHex("#e91e63");
            ProfileImage.Source = "profile_disabled.png";
            ProfileLabel.TextColor = Color.FromHex("#bcbcbc");
        }

        private void Nav_Profile(object sender, EventArgs e)
        {
            Chats.IsVisible = false;
            Profile.IsVisible = true;
            SearchBar.IsVisible = false;
            AlertLabel.IsVisible = false;

            ChatImage.Source = "chat_disabled.png";
            ChatLabel.TextColor = Color.FromHex("#bcbcbc");
            ProfileImage.Source = "profile_enabled.png";
            ProfileLabel.TextColor = Color.FromHex("#e91e63");
        }

        private async void Nav_Result(object sender, EventArgs e)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                await Navigation.PushModalAsync(new SearchResults(), true);
            }
        }

        private async void checkUserContacts()
        {
            string id = dataClass.loggedInUser.uid;
            var firestoreUserContactList = await CrossCloudFirestore.Current.Instance.Collection("users").WhereEqualsTo("uid", id).GetAsync();
            var UserContactList = firestoreUserContactList.ToObjects<UserModel>().FirstOrDefault();

            if (UserContactList != null && UserContactList.contacts.Count != 0) // Naa kay friends
            {
                isFriendsNotExist = false;
                AlertLabel.IsVisible = false;
            } 
        }
    }
}