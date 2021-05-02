using LocalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalaryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetails : ContentPage
    {
        TaskListViewModel vm;
        public UserDetails()
        {
            InitializeComponent();
            vm = new TaskListViewModel();
            this.BindingContext = vm;
        }

       
       
    }
   
}