
using ChatApp.TempData;
using Plugin.CloudFirestore;
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
        bool isFetching = false;
        ObservableCollection<UserResults> userResultsList = new ObservableCollection<UserResults>();
        public class UserResults
        {
            public string id { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string iconSource { get; set; }
        }

        DataClass dataClass = DataClass.GetInstance;

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

        private async void SearchQuery(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                AlertLabel.IsVisible = false;
                return;
            }

            if (!isFetching)
            {
                userListView.IsRefreshing = true;
                await Task.Delay(500);
                fetchSearchResults();
            }
        }

        private async void fetchSearchResults()
        {
            // Bug
            isFetching = true;
            userListView.ItemsSource = null;
            userResultsList.Clear();

            string id = dataClass.loggedInUser.uid;
            var firestoreUserList = await CrossCloudFirestore.Current.Instance.Collection("users").GetAsync();

            var users = firestoreUserList.ToObjects<UserModel>().ToArray().Where(x => x.email.ToLower().Contains(SearchEntry.Text.ToLower()));

            if (users.Count() == 0)
            {
                AlertLabel.IsVisible = true;
                userListView.IsRefreshing = false;
                return;
            }

            AlertLabel.IsVisible = false;
            var userFriends = firestoreUserList.ToObjects<UserModel>().ToArray().Where(x => x.uid == id).First().contacts;

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

            userListView.IsRefreshing = false;
            userListView.ItemsSource = userResultsList;
            isFetching = false;
        }

        private async void AddToContact(object sender, EventArgs e)
        {
            string searchID = (string)((TappedEventArgs)e).Parameter;
            string userID = dataClass.loggedInUser.uid;

            if (searchID.Equals(userID))
            {
                await DisplayAlert("Error", "You are not allowed to add yourself.", "", "OKAY");
                return;
            }

            var userContacts = userResultsList.Where(x => x.id == userID).ToArray();
            if (userContacts.Where(x => x.id == searchID && x.iconSource == "check").Count() == 1)
            {
                await DisplayAlert("Failed", "You already have a connection.", "", "OKAY");
                return;
            }

            var user = userResultsList.Where(x => x.id == searchID).First();
            bool answer = await DisplayAlert("Add Contact", "Would you like to add " + user.username + "?", "YES", "NO");

            if (answer)
            {
                await CrossCloudFirestore.Current
                         .Instance
                         .Collection("users")
                         .Document(userID)
                         .UpdateAsync("contacts", FieldValue.ArrayUnion(user.id));

                var firestoreUserList = await CrossCloudFirestore.Current.Instance.Collection("contacts").WhereArrayContains("contactID", userID).GetAsync();
                var userContact = firestoreUserList.ToObjects<ContactModel>().ToArray().Where(x => x.contactID[0] == userID).FirstOrDefault();

                if (userContact == null)
                {
                    await DisplayAlert("Error", "", "", "OKAY");
                    return;
                }

                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("contacts")
                        .Document(userContact.id)
                        .UpdateAsync("contactID", FieldValue.ArrayUnion(user.id),
                                     "contactEmail", FieldValue.ArrayUnion(user.email), 
                                     "contactName", FieldValue.ArrayUnion(user.username));

                await DisplayAlert("Success", user.username + " is added to your contacts", "", "OKAY");
                fetchSearchResults();
                //SearchEntry.Text = "";
                //SearchEntry.Focus();
            }
        }
    }
}