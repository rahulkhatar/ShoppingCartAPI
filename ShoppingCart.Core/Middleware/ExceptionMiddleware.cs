using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var statusCode = HttpStatusCode.InternalServerError;
            var errors = new List<string> { "An unexpected error occured." };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errors = new List<string> { notFoundException.Message };
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    errors = validationException.ValidationErrors;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errors = new List<string> { "An unexpected error occurred." };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var errorResponse = new
            {
                IsSuccess = false,
                Errors = errors
            };          
            
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(jsonResponse);
        } 
    }    
}
