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

        public App()
        {
            InitializeComponent();

            contactList.Add(new ContactModel()
            {
                id = "1",
                contactID = new string[] { "1", "2", "3", "4", "5" },
                contactName = new string[] { "User Test-1", "User Test-2", "User Test-3", "User Test-4", "User Test-5" },
                contactEmail = new string[] { "utest1@gmail.com", "utest2@gmail.com", "utest3@gmail.com", "utest4@gmail.com", "utest5@gmail.com" },
                created_at = new DateTime()
            });

            GloblalData.contactList = contactList;

            userList.Add(new UserModel() { id = "1", username = "User Test-1", email = "utest1@gmail.com", password = "1234", contacts = new List<string>(new string[] { "2", "3", "4", "5" }), isVerified = true });
            userList.Add(new UserModel() { id = "2", username = "User Test-2", email = "utest2@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = false });
            userList.Add(new UserModel() { id = "3", username = "User Test-3", email = "utest3@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "4", username = "User Test-4", email = "utest4@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "5", username = "User Test-5", email = "utest5@gmail.com", password = "1234", contacts = new List<string>(new string[] { "1" }), isVerified = true });
            userList.Add(new UserModel() { id = "6", username = "User Test-6", email = "utest6@gmail.com", password = "1234", isVerified = true });
            userList.Add(new UserModel() { id = "7", username = "User Test-7", email = "utest7@gmail.com", password = "1234", isVerified = true });
            userList.Add(new UserModel() { id = "8", username = "User Test-8", email = "utest8@gmail.com", password = "1234", isVerified = true });
            userList.Add(new UserModel() { id = "9", username = "User Test-9", email = "utest9@gmail.com", password = "1234", isVerified = true });
            userList.Add(new UserModel() { id = "10", username = "User Test-10", email = "utest10@gmail.com", password = "1234", isVerified = true });

            GloblalData.userList = userList;

            MainPage = new NavigationPage(new MainPage());
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
    }
}
