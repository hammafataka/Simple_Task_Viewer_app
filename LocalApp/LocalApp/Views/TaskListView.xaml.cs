using LocalApp.Models;
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
    public partial class TaskListView : ContentPage
    {
       // TaskListViewModel vm;
        TaskListViewModelSQLite vmsqlite;
        public TaskListView()
        {
            InitializeComponent();
            // vm = new TaskListViewModel();
            // this.BindingContext = vm;
            vmsqlite = new TaskListViewModelSQLite();
            this.BindingContext = vmsqlite;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() => vmsqlite.LoadDataCommand.Execute(null));

        }
    }
}