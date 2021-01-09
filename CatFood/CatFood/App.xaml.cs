using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFood
{
    public partial class App : Application
    {
        static EditingDataBase database;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new FontSizePages());
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
        public static EditingDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new EditingDataBase();
                }
                return database;
            }
        }
    }
}
