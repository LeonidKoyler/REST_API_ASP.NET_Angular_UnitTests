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
        }
    }
}
