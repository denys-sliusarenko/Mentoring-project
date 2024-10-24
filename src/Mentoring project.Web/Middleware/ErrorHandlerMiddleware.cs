﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MentoringProject.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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
                response.ContentType = "application/json";

                switch (error)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { ErrorMessage = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}