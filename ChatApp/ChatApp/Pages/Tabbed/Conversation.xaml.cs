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
    public partial class Conversation : ContentPage
    {
        ObservableCollection<MessageList> messageList = new ObservableCollection<MessageList>();
        public class MessageList
        {
            public string userID { get; set; }
            public string message { get; set; }
            public string row { get; set; }
            public string column { get; set; }
            public string color { get; set; }
            public LayoutOptions alignment { get; set; }
        }
        public Conversation()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            fetchConversation();
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ToggleSendButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MessageEntry.Text))
            {
                SendButton.Source = "send_disabled";
            } else
            {
                SendButton.Source = "send_enabled";
            }
        }

        private void fetchConversation()
        {
            string uid = (string)Application.Current.Properties["id"];
            string kachat = "3"; // change this later
            var conversation = GloblalData.conversationList.Where(
                x => (Array.Exists(x.converseeID, element => element == uid) &&
                Array.Exists(x.converseeID, element => element == kachat))
            ).FirstOrDefault();

            if (conversation == null)
            {
                return;

            }

            for (int i = 0; i < conversation.messages.Count(); i++)
            {
                string row = "40, *";
                string column = "1";
                string color = "#9c27b0";
                LayoutOptions alignment = LayoutOptions.End;

                if (conversation.messages[i].converseeID == kachat)
                {
                    row = "*, 40";
                    column = "0";
                    color = "#e91e63";
                    alignment = LayoutOptions.Start;
                }

                messageList.Add(new MessageList()
                {
                    userID = conversation.messages[i].converseeID,
                    message = conversation.messages[i].message,
                    row = row,
                    column = column,
                    color = color,
                    alignment = alignment
                });
            }

            messageListGrid.IsVisible = true;
            alertLabel.IsVisible = false;
            messageListView.ItemsSource = messageList;
        }
    }
}