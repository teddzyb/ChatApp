
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
        ObservableCollection<UserModel> userResult = new ObservableCollection<UserModel>();
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

        private void SearchQuery(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                userResult.Clear();
                AlertLabel.IsVisible = false;
                return;
            }
            
            FetchSearchResults();
        }

        private async void FetchSearchResults()
        {
            userListView.ItemsSource = null;
            userResult.Clear();

            var documents = await CrossCloudFirestore.Current
                .Instance
                .Collection("users")
                .WhereEqualsTo("email", SearchEntry.Text)
                .GetAsync();

            var userData = documents.ToObjects<UserModel>();

            foreach (var user in userData)
            {
                userResult.Add(user);
            }

            //foreach (var documentChange in documents.DocumentChanges)
            //{
            //    var json = JsonConvert.SerializeObject(documentChange.Document.Data);
            //    var obj = JsonConvert.DeserializeObject<UserModel>(json);

            //    userResult.Add(obj);
            //}

            if (userResult.Count == 0)
            {
                AlertLabel.IsVisible = true;
                userListView.IsRefreshing = false;
                return;
            }

            AlertLabel.IsVisible = false;
            userListView.IsRefreshing = false;
            userListView.ItemsSource = userResult;
        }

        private void ClearResults(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                userResult.Clear();
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
                await DisplayAlert("Error", "You are already added to this contact.", "", "OKAY");
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
                await DisplayAlert("Success", "Contact added successfully.", "", "OKAY");
            }
        }
    }
}