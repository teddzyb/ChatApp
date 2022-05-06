using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentView
    {
        public Profile()
        {
            InitializeComponent();
        }

        private async void Btn_SignOut(object sender, EventArgs e)
        {
            ActivityIndicator.IsRunning = true;
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = DependencyService.Get<IFirebaseAuth>().SignOut();

            if (res.Status != true)
            {
                ActivityIndicator.IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", res.Response, "", "Okay");
                return;
            }

            ActivityIndicator.IsRunning = false;
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}