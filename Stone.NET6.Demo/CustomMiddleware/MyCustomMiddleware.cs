
using System.Runtime.CompilerServices;

namespace Stone.NET6.Demo.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("MyCustomMiddleware - Starts ");
            await next(context);
            await context.Response.WriteAsync("MyCustomMiddleware - Ends ");
        }
    }

    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
           return app.UseMiddleware<MyCustomMiddleware>();            
        }
    }
}
