using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using System;

namespace com.xamarin.sample.splashscreen
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        static readonly string TAG = "X:" + typeof (SplashActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.Splash);
                Log.Debug(TAG, "SplashActivity.OnCreate");
                VerifyPermissions();
            }
            catch (Exception e)
            {

                Log.Debug(TAG, "SplashActivity.OnCreate ==> "+e.Message);
            }
   
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            //VerifyPermissions();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup ()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            // StartActivity(new Intent(Application.Context, typeof (MainActivity)));

        }

        #region Permissions
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 0)
            {
                // Received permission result for camera permission.
                Log.Info(TAG, "Received response for Location permission request.");

                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    // Location permission has been granted, okay to retrieve the location of the device.
                    Log.Info(TAG, "Location permission has now been granted.");
                    StartApp();
                }
                else
                {
                    Log.Info(TAG, "Location permission was NOT granted.");
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteContacts }, 0); 
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        private void StartApp()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        private void VerifyPermissions()
        {
            if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteContacts) == (int)Permission.Granted)
            {
                StartApp();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] {  Manifest.Permission.WriteContacts, }, 0);
            }
        }
        #endregion
    }
}