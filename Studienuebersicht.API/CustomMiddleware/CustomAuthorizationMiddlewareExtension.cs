using Microsoft.AspNetCore.Builder;
using Studienuebersicht.API.CustomMiddleware;

namespace Culture
{
    public static class CustomAuthorizationMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorizationMiddleware>();
        }
    }
}
