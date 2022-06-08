using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;
using TodoApp.Services.DataTables;
using System.Collections.Generic;
using System.Threading;

namespace TodoApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<TaskModel> TaskModels { private set; get; } 
            = new ObservableCollection<TaskModel>();
        public ICommand AddTappedCommand { get; }
        public ICommand TaskTappeCommand { get; }
        public ICommand IsCompleteTappedCommand { get; }

        public MainPageViewModel()
        {
            AddTappedCommand = new Command(async () =>
            {
                if (IsBusy) return;
                IsBusy = true;
                await Application.Current.MainPage.Navigation.PushAsync(new TaskDetailPage());
                IsBusy = false;
            });
            TaskTappeCommand = new Command(async (object obj) =>
            {
                if (IsBusy) return;
                IsBusy = true;
                await Application.Current.MainPage.Navigation.PushAsync(new TaskDetailPage((TaskModel)obj));
                IsBusy = false;
            });
            IsCompleteTappedCommand = new Command((object obj) =>
            {
                if(obj is TaskModel)
                {
                    Task.Run(async () =>
                    {
                        var service = await TodoService.Instance;
                        await service.SaveTask(((TaskModel)obj).Tasktable);
                    });
                }
                
            });
        }

        internal void Refresh()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            Task.Run<List<TaskTable>>(async () =>
            {
                var service = await TodoService.Instance;
                var tasks = await service.GetTasks();
                tasks.Sort((t1, t2) =>
                {
                    if (t1.IsComplete == t2.IsComplete)
                    {
                        return t1.Id < t2.Id ? -1 : 1;
                    }
                    else if (t1.IsComplete)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                });
                return tasks;
            }).
            ContinueInMainThreadWith((taskTables) =>
            {
                TaskModels.Clear();
                if (taskTables != null && taskTables.Count > 0)
                {
                    taskTables.ForEach((t) => { TaskModels.Add(new TaskModel(t)); });
                }

                IsBusy = false;
            });



        }
    }
}
