using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TestProject.Contract;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("tokens")]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] CreateTokenRequest user)
        {
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Password", user.Password)
            };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new CreatedResult("", new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}