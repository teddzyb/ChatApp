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
      
            if (string.IsNullOrEmpty(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Missing Field", "OKAY");
                emailFrame.BorderColor = Color.FromRgb(244, 67, 54);
                return;
            }

            if (!ValidateEmail.IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Error", "The email address is badly formatted.", "", "OKAY");
                return;
            }
            
            ActivityIndicator.IsRunning = true;
            
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<IFirebaseAuth>().ResetPassword(EmailEntry.Text);

            if (res.Status != true) 
            {
                ActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", res.Response, "Okay");
                return;
            }

            ActivityIndicator.IsRunning = false;
            await DisplayAlert("Success", res.Response, "Okay");
            await Navigation.PopAsync();
        }

        private void Focused_Email(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.FromRgb(189, 189, 189);
        }
        
    }
}