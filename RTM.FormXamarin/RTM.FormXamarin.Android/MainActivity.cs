using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RayTrackingMobile;
using RayTrackingMobile.Models;
using PCLAppConfig;

namespace RTM.FormXamarin.Droid
{
    [Activity(Label = "RTM.FormXamarin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
           // ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);

            XF.Material.Droid.Material.Init(this, savedInstanceState);

            this.LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#07485B"));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}