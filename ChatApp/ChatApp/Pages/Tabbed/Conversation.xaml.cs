﻿using ChatApp.TempData;
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

    public partial class Conversation : ContentPage
    {
        ObservableCollection<MessageList> messageList = new ObservableCollection<MessageList>();
        ConversationModel conversation;
        DataClass dataClass = DataClass.GetInstance;
        bool isDataFetched = false;
        string userID;
        string receiverID;

        public class MessageList
        {
            public string userID { get; set; }
            public string message { get; set; }
            public string column { get; set; }
            public ColumnDefinitionCollection columnDef { get; set; }
            public string color { get; set; }
            public LayoutOptions alignment { get; set; }
        } 
        public Conversation(string receiverID)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            userID = dataClass.loggedInUser.uid;
            this.receiverID = receiverID;

            FetchConversation();
            //FetchMessages();
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

        private async void SendMessage(object sender, EventArgs e)
        {
            string message = MessageEntry.Text;

            if (String.IsNullOrWhiteSpace(MessageEntry.Text))
            {
                return;
            }

            message = message.TrimStart().TrimEnd();

            if (conversation == null)
            {
                conversation = new ConversationModel()
                {
                    id = Guid.NewGuid().ToString(),
                    messages = new List<MessageModel> { },
                    converseeID = new string[] { userID, receiverID },
                    created_at = DateTime.UtcNow,
                };

                await CrossCloudFirestore.Current
                        .Instance
                        .Collection("conversations")
                        .Document(conversation.id)
                        .SetAsync(conversation);
            }

            MessageModel newMessage = new MessageModel()
            {
                id = Guid.NewGuid().ToString(),
                message = message,
                converseeID = userID,
                created_at = DateTime.Now
            };

            conversation.messages.Add(newMessage);

            await CrossCloudFirestore.Current
                .Instance
                .Collection("conversations")
                .Document(conversation.id)
                .UpdateAsync("messages", FieldValue.ArrayUnion(newMessage));

            MessageEntry.Text = string.Empty;
            messageList.Clear();
            messageListView.ItemsSource = null;
            FetchConversation();
        }

        private async void FetchConversation()
        {
            if (isDataFetched == false)
            {
                var firestoreConversation = await CrossCloudFirestore.Current.Instance.Collection("conversations")
                                    .WhereArrayContains("converseeID", userID)
                                    .GetAsync();



                var convo = firestoreConversation.ToObjects<ConversationModel>()
                                    .ToArray()
                                    .Where(x => Array.Exists(x.converseeID, y => y == userID) && Array.Exists(x.converseeID, y => y == receiverID))
                                    .FirstOrDefault();

                if (convo == null)
                {
                    AlertLabel.IsVisible = true;
                    isDataFetched = true;
                    return;
                }

                conversation = new ConversationModel()
                {
                    id = convo.id,
                    converseeID = convo.converseeID,
                    messages = convo.messages,
                    created_at = convo.created_at,
                };

                isDataFetched = true;
            }

            messageListView.IsRefreshing = true;

            for (int i = 0; i < conversation.messages.Count(); i++)
            {
                string column = "1";
                string color = "#9c27b0";
                LayoutOptions alignment = LayoutOptions.End;

                ColumnDefinitionCollection columnDef = (ColumnDefinitionCollection)new ColumnDefinitionCollectionTypeConverter().ConvertFromInvariantString("40, *");

                if (conversation.messages[i].converseeID == receiverID)
                {
                    column = "0";
                    columnDef = (ColumnDefinitionCollection)new ColumnDefinitionCollectionTypeConverter().ConvertFromInvariantString("*, 40");
                    color = "#e91e63";
                    alignment = LayoutOptions.Start;
                }

                messageList.Add(new MessageList()
                {
                    userID = conversation.messages[i].converseeID,
                    message = conversation.messages[i].message,
                    column = column,
                    columnDef = columnDef,
                    color = color,
                    alignment = alignment
                });
            }

            messageListGrid.IsVisible = true;
            AlertLabel.IsVisible = false;
            messageListView.ItemsSource = messageList;

            if (messageList.Count > 0)
            {
                messageListView.ScrollTo(messageList[messageList.Count - 1], ScrollToPosition.End, false);
            }

            messageListView.IsRefreshing = false;
        }
    }
}