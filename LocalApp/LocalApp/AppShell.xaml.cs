using LocalApp.ViewModels;
using LocalApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LocalApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
          
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            LoginPage page = new LoginPage();
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
