using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Studienuebersicht.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studienuebersicht.API.CustomMiddleware
{
    public class CustomAuthorizationMiddleware
    {
        private RequestDelegate next;
        private IConfiguration configuration;
        private IRepository repository;

        public CustomAuthorizationMiddleware(RequestDelegate next, IConfiguration configuration, IRepository repository)
        {
            this.next = next;
            this.configuration = configuration;
            this.repository = repository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/Authentication"))
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    SendError(context, "Unauthorized access!");
                    return;
                }

                var account = GetAccountFromToken(token);
                if (account == null)
                {
                    SendError(context, "Account not found!");
                    return;
                }

                context.Items["account"] = account;
            }
            await next(context);
        }

        private void SendError(HttpContext context, string message)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            string json = System.Text.Json.JsonSerializer.Serialize(message);
            context.Response.WriteAsync(json, Encoding.UTF8);
        }

        private Account GetAccountFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                int account_id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var account = repository.Accounts.ById(account_id);
                return account;
            }
            catch
            {
                return null;
            }
        }
    }
}
