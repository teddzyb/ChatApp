using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Plugin.CloudFirestore;
using ChatApp.Droid;
using Xamarin.Forms;
using Android.Gms.Extensions;

[assembly: Dependency(typeof(FirebaseAuthService))]
namespace ChatApp.Droid
{
    public class FirebaseAuthService : IFirebaseAuth
    {

        DataClass dataClass = DataClass.GetInstance;

        public FirebaseAuthResponseModel IsLoggedIn()
        {
            try
            {
                if (FirebaseAuth.Instance.CurrentUser == null)
                {
                    return new FirebaseAuthResponseModel() { Status = false, Response = "Currently Logged In" };

                }
                return new FirebaseAuthResponseModel() { Status = false, Response = "Currently Logged Out" };
            }
            catch (Exception ex)
            {
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
        }

        public async Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdToken(true).AsAsync<GetTokenResult>();
                
                return new FirebaseAuthResponseModel() { Status = true, Response = token.Token };
            }
            catch (FirebaseAuthInvalidUserException ex)
            {
                ex.PrintStackTrace();
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                ex.PrintStackTrace();
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
        }

        public async Task<FirebaseAuthResponseModel> ResetPassword(string email)
        {
            try
            {
                var user = await FirebaseAuth.Instance.FetchSignInMethodsForEmail(email);

                if (user == null)
                {
                    return new FirebaseAuthResponseModel() { Status = false, Response = "Email not found" };
                }

                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);

                return new FirebaseAuthResponseModel() { Status = true, Response = "Recovery link has been sent" };
            }
            catch (Exception ex)
            {
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
        }

        public FirebaseAuthResponseModel SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return new FirebaseAuthResponseModel() { Status = true, Response = "User signed out" };

            }
            catch (FirebaseAuthActionCodeException ex)
            {
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
        }

        public async Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string username, string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                //var token = await user.User.GetIdToken(true).AsAsync<GetTokenResult>();

                return new FirebaseAuthResponseModel() { Status = true, Response = "Account Successfully Created" };

            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                ex.PrintStackTrace();
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                ex.PrintStackTrace();
                return new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
            }
        }

    }
}
    