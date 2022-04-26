using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using AndroidX.AppCompat.App;
using ChatApp.Droid;
using System.Threading.Tasks;

namespace com.xamarin.sample.splashscreen
{
    [Activity(Label = "Convo", Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    StartActivity(typeof(MainActivity));
        //}

        //static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            //Log.Debug(TAG, "SplashActivity.OnCreate");

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.SetNavigationBarColor(Color.White);
            }
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.SetNavigationBarColor(Color.White);
            }
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.SetNavigationBarColor(Color.White);
            }
            //Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(500); // Simulate a bit of startup work.
            //Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}