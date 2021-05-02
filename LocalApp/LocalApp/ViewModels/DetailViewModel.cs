using LocalApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocalApp.ViewModels
{
    public class DetailViewModel:BaseViewModel
    {
        private Tasks selectedTask;

        public Tasks SelectedTask
        {
            get { return selectedTask; }
            set {SetProperty(ref selectedTask , value); }
        }
        public ICommand UpdateCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand AddCommand { private set; get; }
        private async Task Update()
        {
            bool result = await App.DbContext.UdateItems<Tasks>(SelectedTask);
            if (result)
            {

                await App.Current.MainPage.DisplayAlert("Success!", "The task has been Saved successfully", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
                await App.Current.MainPage.DisplayAlert("Failed!", "The Task was NOT Saved ", "OK");
        }
        private async Task Delete()
        {
            if (SelectedTask.id > 0)
            {
                bool confirm = await App.Current.MainPage.DisplayAlert("Confirm.", "are you sure you want to delete this task?", "yes Delete it", "Better Not");
                if (confirm)
                {
                    confirm = await App.DbContext.DeleteItem<Tasks>(SelectedTask);
                    if (confirm)
                    {

                        await App.Current.MainPage.DisplayAlert("Success!", "Task has been deleted successfuly", "OK");
                        await App.Current.MainPage.Navigation.PopAsync();

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Falied!", "Task has been Not deleted ", "OK");

                    }
                }


            }
        }

        private async Task Add()
        {
            
            bool result = await App.DbContext.AddItem<Tasks>(SelectedTask);

            if (result)
            {
        
                await App.Current.MainPage.DisplayAlert("Success!", "The task has been Added successfully", "OK");
                await App.Current.MainPage.Navigation.PopAsync();

            }
            else
                await App.Current.MainPage.DisplayAlert("Failed!", "The Task was NOT Addeed ", "OK");
        }
       
        public DetailViewModel(Tasks tasks)
        {
            SelectedTask = tasks;
            AddCommand = new Command(async () => await Add());
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());

        }
    }
}
