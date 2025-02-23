using Calculation.Contract.Response;
using FluentValidation;
using System.Net;

namespace Calculation.API.Mapping
{
    public class ValidationMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    ErrorsList = ex.Errors.Select(x => new ValidationResponse
                    {
                        PropertyName = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                };

                await context.Response.WriteAsJsonAsync(validationFailureResponse);
            }
            catch (ArgumentException ex) 
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errorResponse = new { Error = ex.Message };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errorResponse = new { Error = $"An unexpected error occurred. {ex.Message}" };

                await context.Response.WriteAsJsonAsync(errorResponse);

            }
        }
    }
}
