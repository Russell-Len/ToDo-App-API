using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo_App_API.DataContext;
using ToDo_App_API.DBHelper;
using ToDo_App_API.Model;

namespace ToDo_App_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ToDoDBHelper _db;

        public class AuthenticationRequestBody
        {
            [Required]
            public string Username { get; set; } = string.Empty;
            [Required]
            public string Password { get; set; } = string.Empty;
        }

        public AuthenticationController(IConfiguration configuration, ToDoDBContext taskDBContext)
        {
            _configuration = configuration;
            _db = new ToDoDBHelper(taskDBContext);
        }

        [HttpPost("login")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = _db.ValidateCredentials(
                authenticationRequestBody.Username,
                authenticationRequestBody.Password
                );

            if (user == null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", user.AuthorId.ToString()),
                new Claim("username", user.Username)
            };

            var jwtToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(tokenToReturn);
        }

        [HttpPost("register")]
        public IActionResult AddAuthor(AuthorToAddModel authorToAddModel)
        {
            try
            {
                bool IsAuthorExists = _db.GetAuthorByUsername(authorToAddModel.Username) != null;

                if (IsAuthorExists) return UnprocessableEntity("Author already exists.");

                _db.AddAuthor(authorToAddModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on the server.");
            }
        }

    }
}
