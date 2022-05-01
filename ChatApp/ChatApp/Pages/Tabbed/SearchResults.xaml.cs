
using ChatApp.TempData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResults : ContentPage
    {
        ObservableCollection<UserResults> userResultsList = new ObservableCollection<UserResults>();
        public class UserResults
        { 
            public string id { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string iconSource { get; set; }
        }

        public SearchResults()
        {
            InitializeComponent();
          
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchEntry.Focus();
        }
        
        private async void GoBack(object sender, EventArgs e)
        {
            MessagingCenter.Send(new MainPage(), "RefreshMainPage");
            await Navigation.PopAsync(true);
        }
        
        private void SearchQuery(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                AlertLabel.IsVisible = false;
                return;
            }

            fetchSearchResults();
        }

        private void fetchSearchResults()
        {
            userListView.ItemsSource = null;
            userResultsList.Clear();

            string id = (string)Application.Current.Properties["id"];
            var users = GlobalData.userList.Where(x => x.email.ToLower().Contains(SearchEntry.Text.ToLower()));
            if (users.Count() == 0)
            {
                AlertLabel.IsVisible = true;
                return;
            }

            AlertLabel.IsVisible = false;
            var userFriends = GlobalData.userList.Where(x => x.uid == id).First().contacts;

            foreach (var user in users)
            {
                string iconSource = "add";

                if (userFriends.Where(x => x == user.uid).Count() > 0)
                {
                    iconSource = "check";
                }

                if (user.uid == id)
                {
                    iconSource = "close_round";
                }

                userResultsList.Add(new UserResults { id = user.uid, username = user.username, email = user.email, iconSource = iconSource });
            }

            userListView.ItemsSource = userResultsList;
        }
    
        private async void AddToContact(object sender, EventArgs e)
        {
            string searchID = (string)((TappedEventArgs)e).Parameter;
            string userID = (string)Application.Current.Properties["id"];

            if (searchID.Equals(userID)) {
                await DisplayAlert("Error", "You are not allowed to add yourself.", "","OKAY");
                return;
            }

            var userContacts = GlobalData.userList.Where(x => x.uid == userID).First().contacts;
            if (userContacts.Where(x => x == searchID).Count() == 1)
            {
                await DisplayAlert("Failed", "You already have a connection.", "", "OKAY");
                return;
            }

            var user = GlobalData.userList.Where(x => x.uid == searchID).First();
            bool answer = await DisplayAlert("Add Contact", "Would you like to add " + user.username + "?", "YES", "NO");

            if (answer)
            {
                foreach (var userList in GlobalData.userList)
                {
                    if (userList.uid == userID)
                    {
                        userList.contacts.Add(searchID);
                        break;
                    }
                }

                foreach (var contact in GlobalData.contactList)
                {
                    if (contact.contactID[0] == userID)
                    {
                        var contactID = contact.contactID.ToList();
                        contactID.Add(user.uid);
                        contact.contactID = contactID.ToArray();

                        var contactName = contact.contactName.ToList();
                        contactName.Add(user.username);
                        contact.contactName = contactName.ToArray();

                        var contactEmail = contact.contactEmail.ToList();
                        contactEmail.Add(user.email);
                        contact.contactEmail = contactEmail.ToArray();

                        break;
                    }
                }

                await DisplayAlert("Success", user.username + " is added to your contacts", "", "OKAY");
                fetchSearchResults();
                //SearchEntry.Text = "";
                //SearchEntry.Focus();
            }
        }
    }

}