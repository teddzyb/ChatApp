using ChatApp.Pages.Tabbed;
using ChatApp.TempData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp
{
    public partial class App : Application
    {
        public static float screenWidth { get; set; }
        public static float screenHeight { get; set; }
        public static float appScale { get; set;  }

        ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();
        ObservableCollection<ContactModel> contactList = new ObservableCollection<ContactModel>();
        ObservableCollection<ConversationModel> conversationList = new ObservableCollection<ConversationModel>();

        public App()
        {
            InitializeComponent();
            initData();

            GloblalData.conversationList = conversationList;

            MainPage = new NavigationPage(new MainPage());

            if (Application.Current.Properties.ContainsKey("id"))
            {
                var MainTabbed = new MainTabbed();
                MainTabbed.BindingContext = new UserModel 
                {
                    username = Application.Current.Properties["username"].ToString(), 
                    email = Application.Current.Properties["email"].ToString() 
                };

                Application.Current.MainPage = new NavigationPage(MainTabbed);
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void initData()
        {
            
            contactList.Add(new ContactModel()
            {
                id = "1",
                contactID = new string[] { "1", "2", "3", "4", "5" },
                contactName = new string[] { "User Test-1", "User Test-2", "User Test-3", "User Test-4", "User Test-5" },
                contactEmail = new string[] { "utest1@gmail.com", "utest2@gmail.com", "utest3@gmail.com", "utest4@gmail.com", "utest5@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "2",
                contactID = new string[] { "2", "1" },
                contactName = new string[] { "User Test-2", "User Test-1" },
                contactEmail = new string[] { "utest2@gmail.com", "utest1@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "3",
                contactID = new string[] { "3", "1" },
                contactName = new string[] { "User Test-3", "User Test-1" },
                contactEmail = new string[] { "utest3@gmail.com", "utest1@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "4",
                contactID = new string[] { "4", "1" },
                contactName = new string[] { "User Test-4", "User Test-1" },
                contactEmail = new string[] { "utest4@gmail.com", "utest1@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "5",
                contactID = new string[] { "5", "1" },
                contactName = new string[] { "User Test-5", "User Test-1" },
                contactEmail = new string[] { "utest5@gmail.com", "utest1@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "6",
                contactID = new string[] { "6" },
                contactName = new string[] { "User Test-6" },
                contactEmail = new string[] { "utest6@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "7",
                contactID = new string[] { "7" },
                contactName = new string[] { "User Test-7" },
                contactEmail = new string[] { "utest7@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "8",
                contactID = new string[] { "8" },
                contactName = new string[] { "User Test-8" },
                contactEmail = new string[] { "utest8@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "9",
                contactID = new string[] { "9" },
                contactName = new string[] { "User Test-9" },
                contactEmail = new string[] { "utest9@gmail.com" },
                created_at = new DateTime()
            });

            contactList.Add(new ContactModel()
            {
                id = "10",
                contactID = new string[] { "10" },
                contactName = new string[] { "User Test-10" },
                contactEmail = new string[] { "utest10@gmail.com" },
                created_at = new DateTime()
            });

            GloblalData.contactList = contactList;

            userList.Add(new UserModel() { uid = "1", username = "User Test-1", email = "utest1@gmail.com", password = "1234", contacts = new List<string>(new string[] { "2", "3", "4", "5" }), isVerified = true });
            userList.Add(new UserModel() { uid = "2", username = "User Test-2", email = "utest2@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = false });
            userList.Add(new UserModel() { uid = "3", username = "User Test-3", email = "utest3@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { uid = "4", username = "User Test-4", email = "utest4@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { uid = "5", username = "User Test-5", email = "utest5@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { uid = "6", username = "User Test-6", email = "utest6@gmail.com", password = "1234", contacts = new List<string>(new string[] { }), isVerified = true });
            userList.Add(new UserModel() { uid = "7", username = "User Test-7", email = "utest7@gmail.com", password = "1234", contacts = new List<string>(new string[] { }), isVerified = true });
            userList.Add(new UserModel() { uid = "8", username = "User Test-8", email = "utest8@gmail.com", password = "1234", contacts = new List<string>(new string[] { }), isVerified = true });
            userList.Add(new UserModel() { uid = "9", username = "User Test-9", email = "utest9@gmail.com", password = "1234", contacts = new List<string>(new string[] { }), isVerified = true });
            userList.Add(new UserModel() { uid = "10", username = "User Test-10", email = "utest10@gmail.com", password = "1234", contacts = new List<string>(new string[] { }), isVerified = true });

            GloblalData.userList = userList;

            ObservableCollection<MessageModel> messageList = new ObservableCollection<MessageModel>();
            messageList.Add(new MessageModel() { id = "1", message = "hello, 3", converseeID = "1", created_at = DateTime.Now });
            messageList.Add(new MessageModel() { id = "2", message = "hello, 1", converseeID = "3", created_at = DateTime.Now });
            messageList.Add(new MessageModel() { id = "3", message = "how you?", converseeID = "3", created_at = DateTime.Now });
            messageList.Add(new MessageModel() { id = "4", message = "good!", converseeID = "1", created_at = DateTime.Now });
            messageList.Add(new MessageModel() { id = "5", message = "great!", converseeID = "3", created_at = DateTime.Now });

            conversationList.Add(new ConversationModel()
            {
                id = "1",
                messages = new List<MessageModel>
                {
                    new MessageModel() { id = "1", message = "hello, 3", converseeID = "1", created_at = DateTime.Now },
                    new MessageModel() { id = "2", message = "hello, 1", converseeID = "3", created_at = DateTime.Now },
                    new MessageModel() { id = "3", message = "how are you?", converseeID = "3", created_at = DateTime.Now },
                    new MessageModel() { id = "4", message = "good, you?", converseeID = "1", created_at = DateTime.Now },
                    new MessageModel() { id = "5", message = "good too, thanks!", converseeID = "3", created_at = DateTime.Now },
                },
                converseeID = new string[] { "1", "3" }
            });
        }
    }
}
