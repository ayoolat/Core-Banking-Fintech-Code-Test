using System.Net;
using System.Text.Json;
using Innovectives.Groups.Business.Layer.Dtos;

namespace Innovectives.Group.Application.Layer.Middlewares
{
    public class ErrorHandlerMiddelware
    {
        public class ErrorHandlerMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ErrorHandlerMiddleware> _logger;

            public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    var response = context.Response;
                    response.ContentType = "application/json";

                    response.StatusCode = GetStatusCode(ex);

                    if (response.StatusCode == 500)
                        await response.WriteAsync(JsonSerializer.Serialize(new { message = "An error occurred" }));

                    var result = JsonSerializer.Serialize(new ErrorDto
                    { 
                        Message = ex?.Message,
                        StatusCode = response.StatusCode,
                    });

                    await response.WriteAsync(result);
                }
            }

            public int GetStatusCode(Exception error)
            {
                int statuscode;
                switch (error)
                {
                    case UnauthorizedAccessException:
                        statuscode = (int)HttpStatusCode.Forbidden;
                        break;
                    case KeyNotFoundException:
                        statuscode = (int)HttpStatusCode.NotFound;
                        break;
                    case ApplicationException:
                        statuscode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Exception:
                        statuscode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        statuscode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                return statuscode;
            }

        }
    }
}
