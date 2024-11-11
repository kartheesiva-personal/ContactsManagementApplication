using System.Net;

namespace ContactsManagementApplication.Server.Middlewares
{
    public class GlobalExceptionMiddleware
    {
            private readonly RequestDelegate _next;

            public GlobalExceptionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext httpContext)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await httpContext.Response.WriteAsync($"Error: {ex.Message}");
                }
            }
        }

    }
