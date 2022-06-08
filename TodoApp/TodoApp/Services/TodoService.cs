using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Services.DataTables;

namespace TodoApp.Services
{

    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/databases
    public class TodoService : ITodoRepository
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoService> Instance = new AsyncLazy<TodoService>(async () =>
        {
            var instance = new TodoService();
            CreateTableResult result = await Database.CreateTableAsync<TaskTable>();
            return instance;
        });


        private TodoService()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<int> DeleteTask(int id)
        {
            return Database.DeleteAsync<TaskTable>(id);
        }

        public Task<TaskTable> GetTask(int id)
        {
            return Database.Table<TaskTable>().Where((task) => task.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<TaskTable>> GetTasks()
        {
            return Database.Table<TaskTable>().ToListAsync();
        }

        public Task<int> SaveTask(TaskTable taskTable)
        {
            if(taskTable.Id != 0)
            {
                return Database.UpdateAsync(taskTable);
            } 
            else
            {
                return Database.InsertAsync(taskTable);
            }
        }
    }
}
