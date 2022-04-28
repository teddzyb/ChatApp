using ChatApp.TempData;
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
        bool isFriendsExist = true;
        
        public MainTabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            string id = (string)Application.Current.Properties["id"];
          
            var contactCount = GloblalData.contactList.Where(x => x.contactID[0] == id).Count();
            
            if (contactCount > 0)
            {
                isFriendsExist = false;
                AlertLabel.IsVisible = false;
            }
        }

        private void Nav_Chat(object sender, EventArgs e)
        {
            Chats.IsVisible = true;
            Profile.IsVisible = false;
            SearchBar.IsVisible = true;
            AlertLabel.IsVisible = isFriendsExist;

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
            await Navigation.PushAsync(new SearchResults(), true);
        }
    }
}