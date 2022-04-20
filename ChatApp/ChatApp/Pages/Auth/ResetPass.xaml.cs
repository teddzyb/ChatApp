using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPass : ContentPage
    {
        public ResetPass()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Btn_Back(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Btn_Reset(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);

            if (string.IsNullOrEmpty(emailEntry.Text))
            {
                
                emailEntry.Focus();
                emailFrame.BorderColor = Color.FromRgb(244, 67, 54);
                return;
            }

            // Successful authentication with database (Insert Future Code Here..)
            if (true) 
            {
                await DisplayAlert("Error", "A recovery link has been to your email address.", "OKAY");
                await Navigation.PopAsync();
            }
            
        }

        private void TextChanged_Email(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }
        
    }
}