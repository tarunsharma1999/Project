using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public ActionResult<string> Get()
        {
            var token = GenerateJSONWebToken(1, "Admin");

            return token == null ? null : token;
        }

        private string GenerateJSONWebToken(int userId, string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                                {
                                new Claim(ClaimTypes.Role, userRole),
                                new Claim("UserId", userId.ToString())
                                };

            var token = new JwtSecurityToken(
                                issuer: "mySystem",
                                audience: "myUsers",
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(10),
                                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
