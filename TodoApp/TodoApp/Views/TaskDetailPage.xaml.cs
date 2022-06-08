using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailPage : ContentPage
    {
        private TaskDetailPageViewModel viewModel;
        public TaskDetailPage(TaskModel task = null)
        {
            InitializeComponent();
            viewModel = new TaskDetailPageViewModel(task);
            BindingContext = viewModel;
        }
    }
}