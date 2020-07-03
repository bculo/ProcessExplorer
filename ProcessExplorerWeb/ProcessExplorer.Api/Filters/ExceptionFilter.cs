using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            var exceptionInterfaces = type.GetInterfaces();
            if (exceptionInterfaces.Any(i => i.Name == nameof(IException)))
            {
                HandleIException(context);
                return;
            }

            if (context.Exception is ArgumentNullException || context.Exception is ArgumentException)
                HandleBadRequest(context);

            HandleUnknownException(context);
        }

        private void HandleBadRequest(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad request",
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleIException(ExceptionContext context)
        {
            var exception = context.Exception as IException;
            context.Result = new BadRequestObjectResult(exception.Exception());
            context.ExceptionHandled = true;
        }
    }
}
