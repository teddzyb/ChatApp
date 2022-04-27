
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

            if (string.IsNullOrEmpty(SearchEntry.Text))
            {
                userListView.ItemsSource = null;
                return;
            }

            userListView.ItemsSource = userList.Where(x => x.email.ToLower().Contains(SearchEntry.Text.ToLower()));
        }
    }
}