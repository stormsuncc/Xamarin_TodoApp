using SQLite;

namespace TodoApp.Services.DataTables
{
    public class TaskTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { set; get; }
        public string Name { set; get; }
        public bool IsComplete { set; get; }
    }
}
