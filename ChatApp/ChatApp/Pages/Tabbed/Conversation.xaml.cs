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
    public partial class Conversation : ContentPage
    {
        public Conversation()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}