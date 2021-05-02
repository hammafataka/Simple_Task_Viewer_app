using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using LocalApp.Models;
using LocalApp.Services;
using LocalApp.Views;

namespace LocalApp.ViewModels
{
    public class TaskListViewModelSQLite:BaseViewModel
    {
        public ObservableCollection<Tasks> taskslist { get; }

        private Tasks selectedTask;
        public Tasks SelectedTask
        {
            get { return selectedTask; }
            set { SetProperty(ref selectedTask , value); }
        }

        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToNewCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        private async Task LoadData()
        {
            List<Tasks> task = await App.DbContext.GetItems<Tasks>();
            taskslist.Clear();
            foreach (Tasks t in task)
            {
                taskslist.Add(t);
            }
        }

        public async Task GoToDetails()
        {
            if (selectedTask != null)
                await GoToNavigation(SelectedTask);
        }
        public async Task TaskGoToNew()
        {
            Tasks task = new Tasks();
            await GoToNavigation(task);
        }

        private async Task GoToNavigation(Tasks task)
        {
            DetailViewModel vm = new DetailViewModel(task);
            TaskDetails view = new TaskDetails
            {
                BindingContext = vm
            };
            await App.Current.MainPage.Navigation.PushAsync(view);
        }




        public TaskListViewModelSQLite()
        {
            taskslist = new ObservableCollection<Tasks>();
            SelectedTask = new Tasks();
            LoadDataCommand = new Command(async()=>await LoadData());
            GoToDetailsCommand = new Command(async () => await GoToDetails());
            GoToNewCommand = new Command(async () => await TaskGoToNew());
        }
    }
}
