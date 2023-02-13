using ClientNotification.Common.Controllers.Abstractions;
using ClientNotification.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Linq;
using ClientNotification.Common.Extensions;

namespace ClientNotification.Common.Filters
{
    internal class RequestApiValidationProblemDetailsResult : IActionResult
    {
        private readonly Func<ActionContext, IActionResult> defaultHandler;

        public RequestApiValidationProblemDetailsResult(Func<ActionContext, IActionResult> defaultHandler)
        {
            this.defaultHandler = defaultHandler;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var controllerTypeInfo = ((ControllerContext)context).ActionDescriptor.ControllerTypeInfo;
            if (!controllerTypeInfo.IsAssignableTo(typeof(BaseApiController)))
            {
                var actionResult = defaultHandler(context);
                await actionResult.ExecuteResultAsync(context);
                return;
            };

            var modelStateEntries = context.ModelState
                                           .Where(e => e.Value.Errors.Count > 0)
                                           .ToArray();
            var errors = new List<string>();

            if (modelStateEntries.Any())
            {
                if (modelStateEntries.Length == 1 && modelStateEntries[0].Value.Errors.Count == 1 && modelStateEntries[0].Key == string.Empty)
                {
                    errors.Add(modelStateEntries[0].Value.Errors[0].ErrorMessage);
                }
                else
                {
                    foreach (var modelStateEntry in modelStateEntries)
                    {
                        foreach (var modelStateError in modelStateEntry.Value.Errors)
                        {
                            errors.Add($"{modelStateEntry.Key}: {modelStateError.ErrorMessage}");
                        }
                    }
                }
            }

            var apiError = new ApiError(HttpStatusCode.BadRequest)
            {
                Message = "Validation Error",
                Errors = errors.Cast<object>().ToList()
            };

            await context.HttpContext.WriteJsonApiErrorAsync(apiError);
        }
    }

}
