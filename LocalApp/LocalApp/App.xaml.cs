using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalApp.Services;
using LocalApp.Views;

namespace LocalApp
{
    public partial class App : Application
    {
        private static serviceSQLite dbContext;

        public static serviceSQLite DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    string dbpath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskApp-v01.db3"
                        );
                    dbContext = new serviceSQLite(dbpath);

                }
                return dbContext;

            }

        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
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
