using System.Net;
using Auth.Applicatoin.DTOs.Resopnse;
using Newtonsoft.Json;

namespace AuthAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {

            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string resultMesage = "error occurred please contact system administrator";

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new GenericResponseClass<object>()
                    {
                        Result = null,
                        ResponseMessage = resultMesage,
                        Status = Auth.Applicatoin.Enums.ResponseStatus.Error
                    }
                ));
            }
        }
    }
}
