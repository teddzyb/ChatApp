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
        ConversationModel conversation;
        string uid;
        string kachat;

        public class MessageList
        {
            public string userID { get; set; }
            public string message { get; set; }
            public string column { get; set; }
            public string color { get; set; }
            public LayoutOptions alignment { get; set; }
        }
        public Conversation(string kachat)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            uid = (string)Application.Current.Properties["id"];
            this.kachat = kachat;
            conversation = GloblalData.conversationList.Where(
                x => (Array.Exists(x.converseeID, element => element == uid) &&
                Array.Exists(x.converseeID, element => element == kachat))
            ).FirstOrDefault();

            FetchConversation();
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

            if (conversation != null && messageList.Count > 0)
            {
                messageListView.ScrollTo(messageList[messageList.Count - 1], ScrollToPosition.End, false);
            }
        }

        private void SendMessage(object sender, EventArgs e)
        {
            string message = MessageEntry.Text;

            if (conversation == null)
            {
                GloblalData.conversationList.Add(new ConversationModel
                {
                    //id = (new id),
                    messages = new List<MessageModel> { },
                    converseeID = new string[] { uid, kachat }
                });

                conversation = GloblalData.conversationList.Where(
                    x => (Array.Exists(x.converseeID, element => element == uid) &&
                    Array.Exists(x.converseeID, element => element == kachat))
                ).FirstOrDefault();

                FetchConversation();
            }

            conversation.messages.Add(new MessageModel
            {
                //id = (new id),
                message = message,
                converseeID = uid,
                created_at = DateTime.Now
            });

            MessageEntry.Text = String.Empty;
            messageList.Clear();
            messageListView.ItemsSource = null;
            FetchConversation();
        }

        private void FetchConversation()
        {
            if (conversation == null)
            {
                return;
            }

            for (int i = 0; i < conversation.messages.Count(); i++)
            {
                string column = "1";
                string color = "#9c27b0";
                LayoutOptions alignment = LayoutOptions.End;

                if (conversation.messages[i].converseeID == kachat)
                {
                    column = "0";
                    color = "#e91e63";
                    alignment = LayoutOptions.Start;
                }

                messageList.Add(new MessageList()
                {
                    userID = conversation.messages[i].converseeID,
                    message = conversation.messages[i].message,
                    column = column,
                    color = color,
                    alignment = alignment
                });
            }

            messageListGrid.IsVisible = true;
            alertLabel.IsVisible = false;
            messageListView.ItemsSource = messageList;

            if (messageList.Count > 0)
            {
                messageListView.ScrollTo(messageList[messageList.Count - 1], ScrollToPosition.End, false);
            }
        }
    }
}