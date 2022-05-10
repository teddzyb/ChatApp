using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using Newtonsoft.Json;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentView
    {
        ObservableCollection<ContactModel> contactList = new ObservableCollection<ContactModel>();
        DataClass dataClass = DataClass.GetInstance;
        bool isLoaded = false;
        public Chats()
        {
            InitializeComponent();
            FetchContacts();

            MessagingCenter.Subscribe<MainPage>(this, "RefreshMainPage", (sender) =>
            {
                //FetchContacts();
                isLoaded = false;
            });

            ContactListView.RefreshCommand = new Command(() =>
            {
                FetchContacts();
            });

            ContactListView.IsRefreshing = false;
        }

        private async void Frame_GoToConvo(object sender, EventArgs e)
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                ContactListView.IsRefreshing = true;
                var contactID = (string)((TappedEventArgs)e).Parameter;

                var document = await CrossCloudFirestore.Current
                                         .Instance
                                         .Collection("contacts")
                                         .Document(contactID)
                                         .GetAsync();
                
                var contact = document.ToObject<ContactModel>();

                var user = new UserModel
                {
                    username = dataClass.loggedInUser.uid == contact.contactID[0] ? contact.contactName[1] : contact.contactName[0],
                };
                
                var conversation = new Conversation(contactID)
                {
                    BindingContext = user
                };
                await Navigation.PushModalAsync(conversation, true);
                ContactListView.IsRefreshing = false;
            }
        }

        private void FetchContacts()
        {
            ContactListView.ItemsSource = null;
            contactList.Clear();
            string id = dataClass.loggedInUser.uid;            

            CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .WhereArrayContains("contactID", id)
                .AddSnapshotListener((snapshot, error) =>
                {
                    ContactListView.IsRefreshing = true;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            //var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            //var obj = JsonConvert.DeserializeObject<ContactModel>(json);

                            var obj = documentChange.Document.ToObject<ContactModel>();

                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    contactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        contactList.Remove(item);
                                    }
                                    break;
                            }
                        }
                    }
                    ContactListView.IsVisible = !(contactList.Count == 0);
                    ContactListView.IsRefreshing = false;
                });

            ContactListView.ItemsSource = contactList;
        }
    }
}