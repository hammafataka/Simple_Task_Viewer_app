using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using LocalApp.Models;
using Xamarin.Forms;
using System.Windows.Input;
using LocalApp.Services;
using System.Threading.Tasks;

namespace LocalApp.ViewModels
{
    public class TaskListViewModel:BaseViewModel
    {
        public ObservableCollection<local> locals { get; }
        private local selectedLocal;

        public local SelectedLocal
        {
            get { return selectedLocal; }
            set {SetProperty(ref selectedLocal , value); }
        }
        private User useR;
        public User user
        {
            get { return useR; }
            set => SetProperty(ref useR, value);

        }
        public string nametodisplay;
        public ICommand LoadDataCommand { private set; get; }
        public ICommand GoToDetailsCommand { private set; get; }
        public ICommand GoToNewCommand { private set; get; }
        public ICommand UpdateCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand SaveDetailsCommand { private set; get; }

        private readonly string controller = "api/locals";
        public async Task LoadData()
        {
            IsBusy = true;
            locals.Clear();
            List<local> localsList = await ApiService.GetItems<local>(controller);
            foreach (local l in localsList)
            {
                locals.Add(l);
            }
            IsBusy = false;
        }
        public async Task GoToDetails()
        {

        }
        public async Task Add()
        {
            int idtemp = locals.Count + 1;
            local localtemp = new local();
            string name = await App.Current.MainPage.DisplayPromptAsync("New Task", "Name", "Ok", "Cancel");
            string id = await App.Current.MainPage.DisplayPromptAsync("New Task", "ID must be bigger then "+idtemp, "Ok", "Cancel");
            string image = await App.Current.MainPage.DisplayPromptAsync("New Task", "image url", "Ok", "Cancel");
            string status = await App.Current.MainPage.DisplayPromptAsync("New Task", "Status please enter True or Flase", "Ok", "Cancel");
            localtemp.id = int.Parse(id);
            localtemp.name = name;
            localtemp.image = image;
            localtemp.status =Boolean.Parse(status);
            bool result = await ApiService.AddItem<local>(controller,localtemp);
            if (result)
                await App.Current.MainPage.DisplayAlert("Success", "The Task has been Added successfluy", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Failed", "The Task has Not been Added", "OK");

        }
        public  async Task Update()
        {
            bool result = await ApiService.UpdateItem<local>(controller, SelectedLocal, selectedLocal.id);
            if (result)
                await App.Current.MainPage.DisplayAlert("Success", "The Task has been Updated successfluy", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Failed", "The Task has Not been Updated", "OK");
        }
        public async Task Delete()
        {
            bool result = await ApiService.DeleteItem<local>(controller, selectedLocal.id);
            if (result)
                await App.Current.MainPage.DisplayAlert("Success", "The Task has been Deleted successfluy", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Failed", "The Task has Not been Deleted", "OK");
        }
        public  async Task save()
        {
            Preferences.Set("name", user.name);
            Preferences.Set("country", user.country);
            Preferences.Set("phone", user.phone);
            Preferences.Set("email", user.email);
            await App.Current.MainPage.DisplayAlert("Success!", "User details updated successfuly", "ok");
        }
        public TaskListViewModel()
        {
            user = new User()
            {
                name = Preferences.Get("name", null),
                country = Preferences.Get("country", null),
                email = Preferences.Get("email", null),
                phone = Preferences.Get("phone", null),
            };
            locals = new ObservableCollection<local>();
            LoadDataCommand = new Command(async () => await LoadData());
            DeleteCommand = new Command(async () => await Delete());
            UpdateCommand = new Command(async () => await Update());
            GoToNewCommand = new Command(async () => await Add());
            SaveDetailsCommand = new Command(async()=>await save());

        }
    }
}
