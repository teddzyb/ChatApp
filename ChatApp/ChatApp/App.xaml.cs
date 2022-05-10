using ChatApp.Pages.Tabbed;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChatApp
{
    public partial class App : Application
    {
        public static float screenWidth { get; set; }
        public static float screenHeight { get; set; }
        public static float appScale { get; set;  }

        DataClass dataClass = DataClass.GetInstance;

        public App()
        {
            InitializeComponent();

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
