using Microsoft.AspNetCore.Http;
using Studienuebersicht.Model;
using System.Threading.Tasks;

namespace Studienuebersicht.MVC.CustomMiddleware
{
    public class CustomAuthorizationMiddleware
    {
        private RequestDelegate next;
        private IRepository repository;

        public CustomAuthorizationMiddleware(RequestDelegate next, IRepository repository)
        {
            this.next = next;
            this.repository = repository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/Authentication"))
            {
                if (context.Session.GetInt32("account_id").HasValue)
                {
                    var account_id = context.Session.GetInt32("account_id").Value;
                    var account = repository.Accounts.ById(account_id);
                    context.Items["account"] = account;
                }
                else
                {
                    context.Response.Redirect("/Authentication/Login");
                }
            }
            await next(context);
        }
    }
}
