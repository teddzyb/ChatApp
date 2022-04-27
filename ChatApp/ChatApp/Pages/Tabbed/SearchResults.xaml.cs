
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
        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();
        ObservableCollection<UserResults> userResultsList = new ObservableCollection<UserResults>();
        public class UserResults
        {
            public string username { get; set; }
            public string email { get; set; }
            public string iconSource { get; set; }
        }

        public SearchResults()
        {
            InitializeComponent();
          
            NavigationPage.SetHasNavigationBar(this, false);
            UserData users = new UserData();
            userList = users.userList;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchEntry.Focus();
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        
        private void SearchQuery(object sender, EventArgs e)
        {
            userListView.ItemsSource = null;
            userResultsList.Clear();

            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                return;
            }

            string id = (string)Application.Current.Properties["id"];            
            var users = userList.Where(x => x.email.ToLower().Contains(SearchEntry.Text.ToLower()));
            if (users.Count() > 0)
            {
                var userFriends = userList.Where(x => x.id == id).First().contacts;

                foreach (var user in users)
                {
                    string iconSource = "add";

                    if (userFriends.Where(x => x == user.id).Count() > 0)
                    {
                        iconSource = "check";                           
                    }

                    if (user.id == id)
                    {
                        iconSource = "close_round";
                    }

                    userResultsList.Add(new UserResults { username = user.username, email = user.email, iconSource = iconSource });
                }

                userListView.ItemsSource = userResultsList;
            }                
                
            
        }
    }
}