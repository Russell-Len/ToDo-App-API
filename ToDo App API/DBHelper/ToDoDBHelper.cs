using ToDo_App_API.DataContext;
using ToDo_App_API.Model;
using Task = ToDo_App_API.Entity.Task;

namespace ToDo_App_API.DBHelper
{
    public class ToDoDBHelper
    {
        private TaskDBContext _context;

        public ToDoDBHelper(TaskDBContext context)
        {
            _context = context;
        }

        public List<TaskModel> GetTasks()
        {
            List<TaskModel> response = new();

            var dataList = _context.Tasks.ToList();
            dataList.ForEach(row => response.Add(new TaskModel()
            {
                Id = row.Id,
                Title = row.Title,
                Description = row.Description,
                DueDate = row.DueDate,
                Category = row.Category,
                IsDeleted = row.IsDeleted,
                Created = row.Created,
                Updated = row.Updated,
            }));

            return response;
        }

        public void SaveTask(TaskModel taskModel)
        {
            Task? taskToSave = new();
           
            if (taskModel.Id > 0)
            {
                //PUT - Update task
                taskToSave = _context.Tasks.Where(d => d.Id.Equals(taskModel.Id)).FirstOrDefault();

                if (taskToSave != null)
                {
                    taskToSave.Title = taskModel.Title;
                    taskToSave.Description = taskModel.Description;
                    taskToSave.DueDate = taskModel.DueDate;
                    taskToSave.Category = taskModel.Category;

                    taskToSave.Updated = DateTime.UtcNow;
                }
            }
            else
            {
                //POST - Add new task
                taskToSave.Title = taskModel.Title;
                taskToSave.Description = taskModel.Description;
                taskToSave.DueDate = taskModel.DueDate;
                taskToSave.Category = taskModel.Category;
                taskToSave.IsDeleted = false;

                taskToSave.Created = DateTime.UtcNow;
                taskToSave.Updated = DateTime.UtcNow;
                _context.Tasks.Add(taskToSave);
            }
            _context.SaveChanges();
        }

        public void SoftDeleteTask(int Id)
        {
            var taskToSoftDelete = _context.Tasks.Where(d => d.Id.Equals(Id)).FirstOrDefault();

            if (taskToSoftDelete != null)
            {
                //PUT - Soft delete task
                taskToSoftDelete.IsDeleted = true;

                _context.SaveChanges();
            }
        }
    }
}
