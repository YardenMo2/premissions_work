using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace premissions_work
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private imageView camera;
        private Button takepic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            initializeViews();
        }

        private void initializeViews()
        {
            takepic = FindViewById<Button>(Resource.Id.btntakepic);
            takepic.Click += Takepic_Click;
        }

        private void Takepic_Click(object sender, System.EventArgs e)
        {
            
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                    StartActivityForResult(intent, 0);
                }
                else
                {
                    // אין אישור להשתמש במצלמה. הצגת הסבר לשימוש הנדרש.
                    if (ShouldShowRequestPermissionRationale(Manifest.Permission.Camera))
                    {
                        Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this)
                            .SetTitle("Camera Permision")
                            .SetMessage("We need the camera to take pictures")
                            .SetPositiveButton("Ok", (sender, args)
                            =>
                            { ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 1); });

                        Dialog dialog = builder.Show();
                    }
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 1);
                }
            
        }


        


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}