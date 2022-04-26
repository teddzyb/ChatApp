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
        public MainTabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void Nav_Chat(object sender, EventArgs e)
        {
            this.FindByName<View>("Chats").IsVisible = true;
            this.FindByName<View>("Profile").IsVisible = false;
            SearchBar.IsVisible = true;

            this.FindByName<Image>("ChatImage").Source = "chat_enabled.png";
            this.FindByName<Label>("ChatLabel").TextColor = Color.FromHex("#e91e63");
            this.FindByName<Image>("ProfileImage").Source = "profile_disabled.png";
            this.FindByName<Label>("ProfileLabel").TextColor = Color.FromHex("#bcbcbc");
        }

        private void Nav_Profile(object sender, EventArgs e)
        {
            this.FindByName<View>("Chats").IsVisible = false;
            this.FindByName<View>("Profile").IsVisible = true;
            SearchBar.IsVisible = false;

            this.FindByName<Image>("ChatImage").Source = "chat_disabled.png";
            this.FindByName<Label>("ChatLabel").TextColor = Color.FromHex("#bcbcbc");
            this.FindByName<Image>("ProfileImage").Source = "profile_enabled.png";
            this.FindByName<Label>("ProfileLabel").TextColor = Color.FromHex("#e91e63");
        }

        private async void Nav_Result(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchResults(), true);
        }
    }
}