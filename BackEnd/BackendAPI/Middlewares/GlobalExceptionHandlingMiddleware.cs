using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using DOMAIN.Exceptions.SQL;
using System;
using DOMAIN.Models.DTR;
using Serilog.Core;
using DOMAIN.Exceptions.Server;
using Microsoft.IdentityModel.Tokens;

namespace BackendAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
         {
            try
            {
                await _next(context);
            }
            catch (SecurityTokenValidationException ex)
            {
                await HandleTokenExceptionAsync(context, ex);
            }
            catch (BaseSqlException ex)
            {
                await HandleSqlExceptionAsync(context, ex);
            }
            catch(BaseServerException ex)
            {
                await HandleServerExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 500;

                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                serviceResponse.Success = false;
                serviceResponse.Message = "ServerSideError";

                await context.Response.WriteAsJsonAsync(serviceResponse);
                _logger.LogError(ex.Message);
            }
        }

        private async Task HandleSqlExceptionAsync(HttpContext context, BaseSqlException exception)
        {
            context.Response.StatusCode = 400;
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Success = false;
            serviceResponse.Message = exception.Message;
            await context.Response.WriteAsJsonAsync(serviceResponse);
        }

        private async Task HandleServerExceptionAsync(HttpContext context, BaseServerException exception)
        {
            context.Response.StatusCode = 403;
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Success = false;
            serviceResponse.Message = exception.Message;
            await context.Response.WriteAsJsonAsync(serviceResponse);
        }

        private async Task HandleTokenExceptionAsync(HttpContext context, SecurityTokenValidationException exception)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Success = false;
            context.Response.StatusCode = 401;
            if (exception is SecurityTokenExpiredException)
            {
                serviceResponse.Message = "TokenExpired";
            }
            else
            {
                serviceResponse.Message = "TokenNotValid";
            }

            await context.Response.WriteAsJsonAsync(serviceResponse);
        }
    }
}
