using LocalApp.Models;
using LocalApp.ViewModels;
using SalaryApp.Views;
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
        TaskListViewModel vm;
        public LoginPage()
        {
            InitializeComponent();
            vm = new TaskListViewModel();
            this.BindingContext = vm;
        }
        protected  override void OnAppearing()
        {
            base.OnAppearing();
            nameLbl.Text = "welcome back" + vm.nametodisplay;
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            UserDetails userDetails = new UserDetails();
            await App.Current.MainPage.Navigation.PushAsync(userDetails);
        }
    }
}