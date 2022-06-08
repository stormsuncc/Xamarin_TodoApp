using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;

namespace TodoApp.ViewModels
{
    internal class TaskDetailPageViewModel
    {
        public TaskModel TaskModel { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public TaskDetailPageViewModel(TaskModel task = null)
        {
            TaskModel = task != null ? task : new TaskModel();

            CancelCommand = new Command(async() => {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            DeleteCommand = new Command(() =>
            {
                Task.Run(async () =>
                {
                    var service = await TodoService.Instance;
                    await service.DeleteTask(TaskModel.Id);

                }).ContinueInMainThreadWith(async() => 
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                });
            });
            SaveCommand = new Command(() => {
                var t = Task.Run(async() => 
                {
                    var service = await TodoService.Instance;
                    await service.SaveTask(TaskModel.Tasktable);
                }).
                ContinueInMainThreadWith(async () =>
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                });
            });
        }



    }
}
