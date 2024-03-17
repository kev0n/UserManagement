using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserStorageService.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

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
            catch (Exception error)
            {
                var response = context.Response;
                var resultMessage = JsonSerializer.Serialize(new {message = error.Message});
                response.ContentType = "application/json";

                switch (error)
                {
                    case NotFoundException _:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    case ArgumentOutOfRangeException _:
                    case CustomException _:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        resultMessage = JsonSerializer.Serialize(new
                        {
                            ValidationErrors = e.Errors.Select(x => x.ErrorMessage)
                                .ToList()
                        });
                        break;
                    default:
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(resultMessage);
            }
        }
    }
}