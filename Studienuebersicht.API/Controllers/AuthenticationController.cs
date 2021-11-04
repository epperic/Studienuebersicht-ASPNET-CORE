using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Studienuebersicht.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Studienuebersicht.API.Controllers
{
    public class AuthenticationController : Controller
    {
        private IConfiguration configuration;
        private IRepository repository;

        public AuthenticationController(IConfiguration configuration, IRepository repository)
        {
            this.configuration = configuration;
            this.repository = repository;
        }

        public IActionResult Index(string email, string password)
        {
            var account = repository.Accounts.FindAccount(email, password);
            if (account == null)
            {
                return new ConflictObjectResult("A user with these credentials could not be found!");
            }
            var token = generateJwtToken(account);
            return new OkObjectResult(new { token = token });
        }

        private string generateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["key"]);
            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) });
            tokenDescriptor.Expires = DateTime.UtcNow.AddDays(7);
            tokenDescriptor.SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
