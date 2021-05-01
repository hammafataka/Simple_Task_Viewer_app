using LocalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        TaskListViewModel welcome;
        public LoginPage()
        {
            InitializeComponent();
            welcome = new TaskListViewModel();
        }
        protected async override void OnAppearing()
        {
   
            base.OnAppearing();
            if (welcome.name == null)
            {
                string name = await DisplayPromptAsync("Welcome", "Plase enter your name..", "OK", "Cancel");
                string country = await DisplayPromptAsync("Welcome", "Plase enter your country name..", "OK", "Cancel");
                welcome.name = name;
                welcome.country = country;
            }
            else
            {
                nameLbl.Text = "Welcome Back " + welcome.name;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("Welcome", "Plase enter your name..", "OK", "Cancel");
            string country = await DisplayPromptAsync("Welcome", "Plase enter your country name..", "OK", "Cancel");
            welcome.name = name;
            welcome.country = country;
            nameLbl.Text = "Welcome Back " + welcome.name;
        }
    }
}