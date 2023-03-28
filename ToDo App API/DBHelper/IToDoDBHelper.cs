using ToDo_App_API.Model;

namespace ToDo_App_API.DBHelper
{
    public interface IToDoDBHelper
    {
        public List<TaskModel> GetTasks(int authorId);

        public void AddTask(TaskToAddModel taskToAddModel);

        public void EditTask(TaskToEditModel taskToEditModel);

        public void SoftDeleteTask(int Id);

        public void AddAuthor(AuthorToAddModel authorToAddModel);

        public AuthorModel? GetAuthorByUsername(string username);

        public AuthorToReturnModel? ValidateCredentials(string username, string password);

        public List<string> GetCategories(int authorId);
    }
}
