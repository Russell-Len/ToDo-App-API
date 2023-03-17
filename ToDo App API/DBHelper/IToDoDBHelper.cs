using ToDo_App_API.Model;

namespace ToDo_App_API.DBHelper
{
    public interface IToDoDBHelper
    {
        public List<TaskModel> GetTasks();

        public void AddTask(TaskModel taskModel);

        public void EditTask(TaskModel taskModel);

        public void SoftDeleteTask(int Id);
    }
}
