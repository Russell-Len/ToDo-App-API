using Microsoft.Build.Framework;

namespace ToDo_App_API.Model
{
    public class AuthorToAddModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
