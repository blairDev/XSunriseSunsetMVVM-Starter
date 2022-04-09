using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XSunriseSunset
{
    public partial class App : Application
    {

        public static string DatabaseLocation = string.Empty;
        public static Location location;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
        }

        public App(string dbLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = dbLocation;
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
