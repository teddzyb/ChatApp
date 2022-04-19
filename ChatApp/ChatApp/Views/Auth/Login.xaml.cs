using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentView
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Btn_SignUp(object sender, EventArgs e)
        {
            this.FindByName<View>("Signup").IsVisible = true;
            this.FindByName<View>("ResetPass").IsVisible = false;
            this.FindByName<View>("LoginGrid").IsVisible = false;
        }

        private void Btn_ResetPass(object sender, EventArgs e)
        {
            this.FindByName<View>("Signup").IsVisible = false;
            this.FindByName<View>("ResetPass").IsVisible = true;
            this.FindByName<View>("LoginGrid").IsVisible = false;
        }
    }
}