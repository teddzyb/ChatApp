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

            userList.Add(new UserModel() { id = 1, username = "edwin", email = "ediwn@gmail.com", password = "123" });
            userList.Add(new UserModel() { id = 2, username = "van", email = "van@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 3, username = "van", email = "van@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 4, username = "van", email = "van@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 5, username = "van", email = "van@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 6, username = "van", email = "van@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 7, username = "van", email = "van@gmail.com", password = "231" });
            userList.Add(new UserModel() { id = 9, username = "van", email = "van@gmail.com", password = "231" });
            


            userListView.ItemsSource = userList;
        }


    }
}