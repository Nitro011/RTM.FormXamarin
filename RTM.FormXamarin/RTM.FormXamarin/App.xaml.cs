using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RTM.FormXamarin.Services;
using RTM.FormXamarin.Views;
using RayTrackingMobile;
using RayTrackingMobile.Models;

namespace RTM.FormXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            XF.Material.Forms.Material.Init(this);
            MainPage = new NavigationPage( new login());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
