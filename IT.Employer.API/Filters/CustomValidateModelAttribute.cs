using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace IT.Employer.WebAPI.Filters
{
    public class CustomValidateModelAttribute : ActionFilterAttribute
    {
        private readonly ILoggerFactory _loggerFactory;
        public CustomValidateModelAttribute(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var modelState = context.ModelState;
            var logger = _loggerFactory.CreateLogger(context.ActionDescriptor.DisplayName);
            if (!modelState.IsValid)
            {
                logger.LogError($"Bad Request: {ModelStateErrorCollector.GetErrors(modelState)}");
                context.Result = new BadRequestObjectResult(modelState);
                return;
            }
            base.OnActionExecuting(context);
        }

    }
}
