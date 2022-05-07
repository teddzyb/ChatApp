using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChatApp.TempData;
using Plugin.CloudFirestore;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chats : ContentView
    {
        DataClass dataClass = DataClass.GetInstance;

        [Obsolete]
        public Chats()
        {
            InitializeComponent();
            FetchContacts();

            MessagingCenter.Subscribe<MainPage>(this, "RefreshMainPage", (sender) =>
            {
                FetchContacts();
            });

            userListView.RefreshCommand = new Command(() =>
            {
                FetchContacts();
            });

            userListView.IsRefreshing = false;
        }

        private async void Frame_GoToConvo(object sender, EventArgs e)
        {
            var receiverID = (string)((TappedEventArgs)e).Parameter;


            var firestoreContact = await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("users")
                                     .WhereEqualsTo("uid", receiverID)
                                     .GetAsync();

            var contact = firestoreContact.ToObjects<UserModel>().ToArray();
            
            var user = new UserModel
            {
                email = contact[0].email,
            };

            var conversation = new Conversation(receiverID)
            {
                BindingContext = user
            };
            await Navigation.PushAsync(conversation, true);
        }

        [Obsolete]
        private async void FetchContacts()
        {
            userListView.IsRefreshing = true;
          
            string id = dataClass.loggedInUser.uid;
            var firestoreContactList = await CrossCloudFirestore.Current.Instance.Collection("contacts").WhereArrayContains("contactID", id).GetDocumentsAsync();
            ContactListGrid.IsVisible = true;

            var contactList = firestoreContactList.ToObjects<ContactModel>().ToArray();

            if (contactList[0].contactID.Count() == 1)
            {
                ContactListGrid.IsVisible = false;
                userListView.IsRefreshing = false;
                return;
            }

            ObservableCollection<UserModel> userContacts = new ObservableCollection<UserModel>();

            for (int i = 1; i < contactList[0].contactID.Count(); i++)
            {
                userContacts.Add(new UserModel() { uid = contactList[0].contactID[i], username = contactList[0].contactName[i], email = contactList[0].contactEmail[i] });
            }

            userListView.ItemsSource = userContacts;
            userListView.IsRefreshing = false;
        }
    }
}