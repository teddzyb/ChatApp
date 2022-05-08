using ChatApp.TempData;
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
        ObservableCollection<ConversationModel> conversationList = new ObservableCollection<ConversationModel>();
        DataClass dataClass = DataClass.GetInstance;
        string contactID;

        public Conversation(string contactID)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.contactID = contactID;

            FetchConversation();        }

        private void FetchConversation()
        {
            CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .Document(contactID)
                .Collection("conversations")
                .OrderBy("createdAt", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var obj = documentChange.Document.ToObject<ConversationModel>();
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    conversationList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        conversationList.Remove(item);
                                    }
                                    break;
                            }

                            if (conversationListView != null)
                            {
                                var conv = conversationListView.ItemsSource.Cast<object>().LastOrDefault();
                                conversationListView.ScrollTo(conv, ScrollToPosition.End, false);
                            }
                        }
                        AlertLabel.IsVisible = conversationList.Count == 0;
                        conversationListView.IsVisible = !(conversationList.Count == 0);
                    }
                });
            conversationListView.ItemsSource = conversationList;
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

            //if (messageList != null && messageList.Count > 0)
            //{
            //    conversationListView.ScrollTo(messageList[messageList.Count - 1], ScrollToPosition.End, false);
            //}
        }

        private async void SendMessage(object sender, EventArgs e)
        {
            ConversationModel conversation = new ConversationModel()
            {
                id = Guid.NewGuid().ToString(),
                converseeID = dataClass.loggedInUser.uid,
                message = MessageEntry.Text,
                createdAt = DateTime.UtcNow
            };
            
            await CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .Document(contactID)
                .Collection("conversations")
                .Document(conversation.id)
                .SetAsync(conversation);

            MessageEntry.Text = string.Empty;
        }
    }
}