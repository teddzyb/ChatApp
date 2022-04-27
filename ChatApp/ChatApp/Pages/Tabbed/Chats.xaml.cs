using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChatApp.TempData;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentView
    {
        ObservableCollection<ConversationModel> chatList = new ObservableCollection<ConversationModel>();
        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();

        public Chats()
        {
            InitializeComponent();

            UserData users = new UserData();
            userListView.ItemsSource = users.userList;
        }

        private async void Frame_GoToConvo(object sender, EventArgs e)
        {

            var user = new UserModel
            {
                email = (String)((TappedEventArgs)e).Parameter,
            };

            var secondPage = new Conversation();

            secondPage.BindingContext = user;
            await Navigation.PushAsync(secondPage, true);
        }

    }
}