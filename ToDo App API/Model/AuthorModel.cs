namespace ToDo_App_API.Model
{
    public class AuthorModel
    {
        public int AuthorId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
