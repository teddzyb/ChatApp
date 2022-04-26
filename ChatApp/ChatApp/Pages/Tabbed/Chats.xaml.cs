using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentView
    {
        ObservableCollection<ChatModel> chatList = new ObservableCollection<ChatModel>();
        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();


        public Chats()
        {
            InitializeComponent();

            //chatList.Add(new ChatModel() { id = 1, senderEmail = "edwin@gmail.com", receiverEmail = "van@gmail,com", message = "Hi wazzup" });
            //chatList.Add(new ChatModel() { id = 2, senderEmail = "edwin@gmail.com", receiverEmail = "van@gmail,com", message = "HELLO wazzup" });
            //chatListView.ItemsSource = chatList;

            userList.Add(new UserModel() { id = 1, username = "User Test-1", email = "utest1@gmail.com", password = "123" });
            userList.Add(new UserModel() { id = 2, username = "User Test-2", email = "utest2gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 3, username = "User Test-3", email = "utest3@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 4, username = "User Test-4", email = "utest4@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 5, username = "User Test-5", email = "utest5@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 6, username = "User Test-6", email = "utest6@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 7, username = "User Test-7", email = "utest7@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 8, username = "User Test-8", email = "utest8@gmail.com", password = "231" });
            


            userListView.ItemsSource = userList;
        }

        private async void Frame_GoToConvo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Conversation(), true);
        }

    }
}