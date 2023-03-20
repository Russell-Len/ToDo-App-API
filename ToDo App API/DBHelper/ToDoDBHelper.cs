using ToDo_App_API.DataContext;
using ToDo_App_API.Model;
using Task = ToDo_App_API.Entity.Task;

namespace ToDo_App_API.DBHelper
{
    public class ToDoDBHelper : IToDoDBHelper
    {
        private readonly TaskDBContext _context;

        public ToDoDBHelper(TaskDBContext context)
        {
            _context = context;
        }

        public List<TaskModel> GetTasks()
        {
            List<TaskModel> response = new();

            var dataList = _context.Tasks.Where(t => !t.IsDeleted).ToList();
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

        public void AddTask(TaskModel taskModel)
        {
            var taskToAdd = new Task
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                DueDate = taskModel.DueDate,
                Category = taskModel.Category,
                IsDeleted = false,

                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _context.Tasks.Add(taskToAdd);

            _context.SaveChanges();

        }

        public void EditTask(TaskModel taskModel)
        {
            var taskToEdit = _context.Tasks.Where(d => d.Id.Equals(taskModel.Id)).FirstOrDefault();

            if (taskToEdit != null)
            {
                taskToEdit.Title = taskModel.Title;
                taskToEdit.Description = taskModel.Description;
                taskToEdit.DueDate = taskModel.DueDate;
                taskToEdit.Category = taskModel.Category;

                taskToEdit.Updated = DateTime.UtcNow;
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
