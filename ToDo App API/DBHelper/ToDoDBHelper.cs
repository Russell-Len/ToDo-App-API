using ToDo_App_API.DataContext;
using ToDo_App_API.Entity;
using ToDo_App_API.Model;
using ToDo_App_API.Utilities;
using Task = ToDo_App_API.Entity.Task;

namespace ToDo_App_API.DBHelper
{
    public class ToDoDBHelper : IToDoDBHelper
    {
        private readonly ToDoDBContext _context;
        private readonly PasswordHasher _passwordHasher = new();

        public ToDoDBHelper(ToDoDBContext context) { _context = context; }

        public List<TaskModel> GetTasks(int authorId)
        {
            List<TaskModel> response = new();

            var dataList = _context.Tasks
                .Where(task => !task.IsDeleted && task.AuthorId == authorId)
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
            DateTime now = DateTime.UtcNow;

            var taskToAdd = new Task
            {
                Title = taskToAddModel.Title,
                Description = taskToAddModel.Description,
                DueDate = taskToAddModel.DueDate,
                Category = taskToAddModel.Category,
                IsDeleted = false,

                Created = now,
                Updated = now,

                AuthorId = taskToAddModel.AuthorId
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

        public void AddAuthor(AuthorToAddModel authorToAddModel)
        {
            var authorToAdd = new Author
            {
                Username = authorToAddModel.Username,
                Password = _passwordHasher.HashPassword(authorToAddModel.Password),
            };

            _context.Authors.Add(authorToAdd);

            _context.SaveChanges();
        }

        public AuthorModel? GetAuthorByUsername(string username)
        {
            var author = _context.Authors.Where(a => a.Username.Equals(username)).FirstOrDefault();

            if (author == null) return null;

            return new AuthorModel
            {
                AuthorId = author.AuthorId,
                Username = author.Username,
                Password = author.Password,
            };
        }

        public AuthorToReturnModel? ValidateCredentials(string username, string password)
        {
            var author = GetAuthorByUsername(username);

            //Return null early if username not found
            if (author == null) return null;

            (bool Verified, _) = _passwordHasher.PasswordCheck(author.Password, password);

            //Return null early if password not valid
            if (!Verified) return null;

            return new AuthorToReturnModel
            {
                AuthorId = author.AuthorId,
                Username = author.Username
            };
        }
    }
}
