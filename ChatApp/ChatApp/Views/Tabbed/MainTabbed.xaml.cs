using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbed : ContentPage
    {
        public MainTabbed()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void Nav_Chat(object sender, EventArgs e)
        {
            this.FindByName<View>("Chat").IsVisible = true;
            this.FindByName<View>("Profile").IsVisible = false;
        }

        private void Nav_Profile(object sender, EventArgs e)
        {
            this.FindByName<View>("Chat").IsVisible = false;
            this.FindByName<View>("Profile").IsVisible = true;
        }
    }
}