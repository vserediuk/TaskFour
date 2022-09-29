using Microsoft.AspNetCore.Identity;
using Task4.Areas.Identity.Data.Models;

namespace Task4.Middleware
{
    public class UserDisablerMiddleware
    {
        private readonly RequestDelegate _next;

        public UserDisablerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            var user = await userManager.GetUserAsync(httpContext.User);

            if (user != null)
            {
                if (user.LockoutEnabled)
                {

                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/");
                }
            }
            else
            {
                if(signInManager.IsSignedIn(httpContext.User))
                {
                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/");
                }
            }

            await _next(httpContext);
        }
    }

        public static class UserDisablerMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserDisablerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserDisablerMiddleware>();
        }
    }
}
