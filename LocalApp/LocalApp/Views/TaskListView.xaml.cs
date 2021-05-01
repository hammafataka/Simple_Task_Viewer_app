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
        TaskListViewModel vm;
        public TaskListView()
        {
            InitializeComponent();
            vm = new TaskListViewModel();
            this.BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(() => vm.LoadDataCommand.Execute(null));
            vm.SelectedLocal = null;

        }
    }
}