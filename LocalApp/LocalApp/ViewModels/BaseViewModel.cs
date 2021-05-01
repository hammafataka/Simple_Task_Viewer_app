using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace LocalApp.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnpropertyChanged([CallerMemberName]string propertyname="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T storage ,T value,[CallerMemberName]string propertyName = "")
        {
            if (object.Equals(storage, value))
                return false;
            storage = value;
            OnpropertyChanged(propertyName);
            return true;
        }
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set {SetProperty(ref isBusy,value);}
        }
        


    }
}
