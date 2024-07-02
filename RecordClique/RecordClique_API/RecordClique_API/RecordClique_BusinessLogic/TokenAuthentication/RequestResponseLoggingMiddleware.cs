using Microsoft.AspNetCore.Http;
using Serilog;

namespace RecordClique_BusinessLogic.TokenAuthentication
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information("Handling request: " + context.Request.Method + " " + context.Request.Path);

            try
            {
                var originalBodyStream = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await _next(context);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                Log.Information("Response: " + text); 
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error is:");
                throw; 
            }
        }
    }
}
