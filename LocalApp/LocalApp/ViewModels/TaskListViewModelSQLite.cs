using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UIKit;


using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using LocalApp.Models;
using LocalApp.Services;
using LocalApp.Views;
using Xamarin.Essentials;
using System.IO;
using System.Net;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace LocalApp.ViewModels
{
    public class TaskListViewModelSQLite:BaseViewModel
    {
        public TaskListViewModel vms;
    

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
        public Image GetImage()
        {
            Image image = new Image
            {
                Source = SelectedTask.image
            };
            return image;
        }
 


        public async Task GoToDetails()
        {

            string option = await App.Current.MainPage.DisplayActionSheet("Select..", "Cancel",null,"Speak the details", "Copy", "Share Via SMS", "Share Via Email", "Share Photo","Go To Details");
            switch (option)
            {

                case "Cancel":
                    Console.WriteLine("It is 1");
                    break;

                case "Copy":
                    await Clipboard.SetTextAsync(selectedTask.name);
                    break;
                case "Share Via SMS":
                    await Sms.ComposeAsync(new SmsMessage(selectedTask.name + "\n" + selectedTask.status, vms.user.phone));
                    break;
                case "Share Via Email":
                    await Email.ComposeAsync("Task Details",selectedTask.name + "\n" + selectedTask.status, vms.user.email);
                    break;
                case "Share Photo":
                    GetImage();
                
                    break;
                case "Go To Details":
                    if (selectedTask != null)
                        await GoToNavigation(SelectedTask);
                    break;
                case "Speak the details":
                    await TextToSpeech.SpeakAsync("Task name is " + selectedTask.name + " and the Current status is " + selectedTask.status);
                    break;
            }
          
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
            vms = new TaskListViewModel();
            taskslist = new ObservableCollection<Tasks>();
            SelectedTask = new Tasks();
            LoadDataCommand = new Command(async()=>await LoadData());
            GoToDetailsCommand = new Command(async () => await GoToDetails());
            GoToNewCommand = new Command(async () => await TaskGoToNew());
        }
    }
}
