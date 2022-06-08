using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TodoApp.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        private bool isBusy;
        protected bool IsBusy
        {
            set { isBusy = value; }
            get => isBusy;
        }
        protected bool IsNotBusy
        {
            get { return !IsBusy; }
        }


    }
}
