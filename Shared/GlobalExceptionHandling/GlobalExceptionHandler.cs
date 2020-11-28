using Microsoft.AspNetCore.Builder;

namespace Shared.GlobalExceptionHandling
{
    public static class GlobalExceptionHandler
    {
        public static void GlobalExceptionHandlerPipeline(this IApplicationBuilder app)
        {
            //app.UseMiddleware<DatabaseExceptionMiddleware>();
        }
    }
}