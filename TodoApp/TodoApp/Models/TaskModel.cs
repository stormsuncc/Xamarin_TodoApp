using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Services.DataTables;

namespace TodoApp.Models
{
    public class TaskModel
    {
        private TaskTable taskTable;
        public TaskTable Tasktable { get => taskTable; set { taskTable = value; } }

        public TaskModel(TaskTable task = null)
        {
            Tasktable = task != null ? task : new TaskTable();
        }

        public string Name
        { 
            get => Tasktable.Name; 
            set 
            {
                taskTable.Name = value;
            } 
        }

        public int Id { get => Tasktable.Id; }

        public bool IsComplete
        {
            get => Tasktable.IsComplete;
            set
            {
                taskTable.IsComplete = value;
            }
        }
    }
}
