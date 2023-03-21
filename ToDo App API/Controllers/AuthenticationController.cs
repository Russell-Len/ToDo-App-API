using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDo_App_API.Controllers
{
    [ApiController]
    [Route("api/authenticaiton")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        private class TodoUser
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
        }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredentials(
                authenticationRequestBody.Username,
                authenticationRequestBody.Password);

            if (user == null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
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

        private TodoUser ValidateUserCredentials(string? username, string? password)
        {
            //check user/password with db

            return new TodoUser();
        }

    }
}
