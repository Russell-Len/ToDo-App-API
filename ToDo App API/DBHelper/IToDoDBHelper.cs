using ToDo_App_API.Model;

namespace ToDo_App_API.DBHelper
{
    public interface IToDoDBHelper
    {
        public List<TaskModel> GetTasks();

        public void AddTask(TaskToAddModel taskToAddModel);

        public void EditTask(TaskToEditModel taskToEditModel);

        public void SoftDeleteTask(int Id);
    }
}
