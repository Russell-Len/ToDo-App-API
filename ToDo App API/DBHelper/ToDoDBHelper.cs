using ToDo_App_API.DataContext;
using ToDo_App_API.Model;
using Task = ToDo_App_API.Entity.Task;

namespace ToDo_App_API.DBHelper
{
    public class ToDoDBHelper : IToDoDBHelper
    {
        private readonly ToDoDBContext _context;

        public ToDoDBHelper(ToDoDBContext context)
        {
            _context = context;
        }

        public List<TaskModel> GetTasks()
        {
            List<TaskModel> response = new();

            var dataList = _context.Tasks
                .Where(task => !task.IsDeleted)
                .OrderBy(task => task.DueDate)
                .ToList();

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

        public void AddTask(TaskToAddModel taskToAddModel)
        {
            var taskToAdd = new Task
            {
                Title = taskToAddModel.Title,
                Description = taskToAddModel.Description,
                DueDate = taskToAddModel.DueDate,
                Category = taskToAddModel.Category,
                IsDeleted = false,

                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            _context.Tasks.Add(taskToAdd);

            _context.SaveChanges();

        }

        public void EditTask(TaskToEditModel taskToEditModel)
        {
            var taskToEdit = _context.Tasks.Where(d => d.Id.Equals(taskToEditModel.Id)).FirstOrDefault();

            if (taskToEdit != null)
            {
                taskToEdit.Title = taskToEditModel.Title;
                taskToEdit.Description = taskToEditModel.Description;
                taskToEdit.DueDate = taskToEditModel.DueDate;
                taskToEdit.Category = taskToEditModel.Category;

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
