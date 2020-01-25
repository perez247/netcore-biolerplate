using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.Exceptions;
using Application.Infrastructure.RequestResponsePipeline;
using Microsoft.AspNetCore.Hosting;
using Application.Interfaces.IExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class WebCustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _env;

        public WebCustomExceptionFilter(IHostingEnvironment ienv)
        {
            _env = ienv;
        }
        public override void OnException(ExceptionContext context)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            if (context.Exception is Application.Exceptions.CustomValidationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    new ErrorResponse() {
                        Errors = ((Application.Exceptions.CustomValidationException)context.Exception).Failures
                    }
                );

                return;
            }

            var code = (int)HttpStatusCode.InternalServerError;
            var exception = context.Exception as IGeneralException;

            if (exception != null )
            {
                code = (int)exception.StatusCode;
            }
            var errorresponse = new ErrorResponse() { Error =  context.Exception.Message };

            errorresponse.StackTrace = _env.IsDevelopment() || _env.IsStaging() ? context.Exception.StackTrace : null;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = code;
            context.Result = new JsonResult(errorresponse, jsonSerializerSettings);
        }
    }
}
