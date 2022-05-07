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

        ObservableCollection<ConversationModel> conversationList = new ObservableCollection<ConversationModel>();
        DataClass dataClass = DataClass.GetInstance;

        [Obsolete]
        public App()
        {
            InitializeComponent();

            GlobalData.conversationList = conversationList;

            MainPage = new NavigationPage(new MainPage());

            if (dataClass.isSignedIn)
            {
                var MainTabbed = new MainTabbed();
                MainTabbed.BindingContext = new UserModel
                {
                    username = dataClass.loggedInUser.username,
                    email = dataClass.loggedInUser.email,
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

    }
}
