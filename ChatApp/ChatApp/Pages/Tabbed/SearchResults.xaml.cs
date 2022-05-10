
using ChatApp.TempData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        ObservableCollection<UserModel> filteredList = new ObservableCollection<UserModel>();
        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();

        DataClass dataClass = DataClass.GetInstance;
        bool isFetched = false;

        public SearchResults()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            fetchLatestContacts();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchEntry.Focus();
        }

        private async void fetchLatestContacts()
        {
            var currentUser = await CrossCloudFirestore.Current
                    .Instance
                    .Collection("users")
                    .Document(dataClass.loggedInUser.uid)
                    .GetAsync();

            var currentUserState = currentUser.ToObject<UserModel>();
            dataClass.loggedInUser.contacts = currentUserState.contacts;
        }            

        private async void GoBack(object sender, EventArgs e)
        {
            //MessagingCenter.Send(new MainPage(), "RefreshMainPage");
            await Navigation.PopModalAsync(true);
        }

        private void SearchQuery(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                filteredList.Clear();
                AlertLabel.IsVisible = false;
                return;
            }

            FetchSearchResults();
        }
        
        private async void FetchSearchResults()
        {
            userListView.IsRefreshing = true;
            userListView.ItemsSource = null;
            filteredList.Clear();

            if (isFetched == false)
            {
                isFetched = true;
                var documents = await CrossCloudFirestore.Current
                    .Instance
                    .Collection("users")
                    .GetAsync();

                //foreach (var documentChange in documents.DocumentChanges)
                //{
                //    var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                //    var obj = JsonConvert.DeserializeObject<UserModel>(json);

                //    userResult.Add(obj);
                //}
                
                var userData = documents.ToObjects<UserModel>();
                
                foreach (var user in userData)
                {
                    userList.Add(user);
                }
            }

            foreach (var user in userList.Where(user => user.email.ToLower().Contains(SearchEntry.Text.ToLower())))
            {
                filteredList.Add(user);
            }

            if (filteredList.Count == 0)
            {
                AlertLabel.IsVisible = true;
                userListView.IsRefreshing = false;
                return;
            }

            AlertLabel.IsVisible = false;
            userListView.IsRefreshing = false;
            userListView.ItemsSource = filteredList;
        }

        private void ClearResults(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                filteredList.Clear();
                AlertLabel.IsVisible = false;
            }
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

            if (dataClass.loggedInUser.contacts.Contains(searchID))
            {
                await DisplayAlert("Failed", "You both already have a connection", "", "OKAY");
                return;
            }

            var document = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("users")
                                     .Document(searchID)
                                     .GetAsync();

            var toAddUser = document.ToObject<UserModel>();

            bool answer = await DisplayAlert("Add Contact", "Would you like to add " + toAddUser.username + "?", "YES", "NO");
            if (answer)
            {
                ContactModel contact = new ContactModel()
                {
                    id = Guid.NewGuid().ToString(),
                    contactID = new string[] { dataClass.loggedInUser.uid, toAddUser.uid },
                    contactEmail = new string[] { dataClass.loggedInUser.email, toAddUser.email },
                    contactName = new string[] { dataClass.loggedInUser.username, toAddUser.username },
                    createdAt = DateTime.UtcNow,
                };

                await CrossCloudFirestore.Current
                    .Instance
                    .Collection("contacts")
                    .Document(contact.id)
                    .SetAsync(contact);
                
                dataClass.loggedInUser.contacts.Add(toAddUser.uid);

                await CrossCloudFirestore.Current
                    .Instance
                    .Collection("users")
                    .Document(dataClass.loggedInUser.uid)
                    .UpdateAsync(new { contacts = dataClass.loggedInUser.contacts });

                toAddUser.contacts.Add(userID);

                await CrossCloudFirestore.Current
                    .Instance
                    .Collection("users")
                    .Document(toAddUser.uid)
                    .UpdateAsync(new { contacts = toAddUser.contacts });

                FetchSearchResults();
                await DisplayAlert("Success", "You are now connected with " + toAddUser.username + ".", "", "OKAY");
            }
        }
    }
}