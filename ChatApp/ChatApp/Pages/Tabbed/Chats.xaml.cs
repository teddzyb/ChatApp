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
    

        public Chats()
        {
            InitializeComponent();
            chatList.Add(new ChatModel() { id = 1, senderEmail = "edwin@gmail.com", receiverEmail = "van@gmail,com", message = "Hi wazzup" });
            chatListView.ItemsSource = chatList;
        }

        
    }
}