namespace ToDo_App_API.Model
{
    public class TaskToEditModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public string Category { get; set; } = string.Empty;
    }
}
