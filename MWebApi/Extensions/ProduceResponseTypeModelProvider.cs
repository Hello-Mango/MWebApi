using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using static Microsoft.AspNetCore.Mvc.Controller;
using MWebApi.Filters;
using MWebApi.Core;

namespace MWebApi.Extensions
{
    public class ProduceResponseTypeModelProvider : IApplicationModelProvider
    {
        public int Order => 0;
        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {

        }
        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                foreach (ActionModel action in controller.Actions)
                {
                    Type type = typeof(ErrorResponse);
                    action.Filters.Add(new ProducesResponseTypeAttribute(type, StatusCodes.Status422UnprocessableEntity));
                    action.Filters.Add(new ProducesResponseTypeAttribute(type, StatusCodes.Status500InternalServerError));
                    if (action.ActionMethod.ReturnType != null)
                    {
                        action.Filters.Add(new ProducesResponseTypeAttribute(action.ActionMethod.ReturnType, StatusCodes.Status200OK));
                    }
                }
            }
        }
    }
}
