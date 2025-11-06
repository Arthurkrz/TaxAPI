using RegistroNF.Core.Common;
using System.Text.Json;

namespace RegistroNF.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try { await _next(context); }
            
            catch (BusinessRuleException brex)
            {
                _logger.LogError(brex, "Business rule violation: {Message}", brex.Message);
                await HandleBusinessRuleException(context, brex);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro inesperado ocorreu: {Message}", ex.Message);
                await HandleUnexpectedException(context, ex);
            }
        }

        public async Task HandleBusinessRuleException(HttpContext context, BusinessRuleException brex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                success = false,
                error = new
                {
                    message = brex.Message,
                    code = brex.ErrorCode ?? "BUSINESS_ERROR"
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        public async Task HandleUnexpectedException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                error = "InternalError",
                title = "Internal Server Error",
                message = "Ocorreu um erro inesperado."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
