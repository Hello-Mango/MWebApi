using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;
using QuickFire.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using QuickFire.Infrastructure;

namespace QuickFireApi.Extensions
{
    public class ProduceResponseTypeModelProvider : IApplicationModelProvider
    {
        public ProduceResponseTypeModelProvider()
        {
        }
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
                    if (action.ActionMethod.ReturnType.IsGenericType && action.ActionMethod.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        // The method returns Task<T>, where T is the generic type argument.
                        Type taskResultType = action.ActionMethod.ReturnType.GetGenericArguments()[0];
                        action.Filters.Add(new ProducesResponseTypeAttribute(taskResultType, StatusCodes.Status200OK));
                    }
                    else if (action.ActionMethod.ReturnType == typeof(Task))
                    {
                        // The method returns Task without a result type.
                        action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                    }
                    else
                    {
                        action.Filters.Add(new ProducesResponseTypeAttribute(action.ActionMethod.ReturnType,StatusCodes.Status200OK));
                    }
                    //if (action.ActionMethod.ReturnType != null)
                    //{
                    //    if (action.ActionMethod.ReturnType == typeof(Task<>))
                    //    {
                    //        action.Filters.Add(new ProducesResponseTypeAttribute(action.ActionMethod.ReturnType.GenericTypeArguments[0], StatusCodes.Status200OK));
                    //    }
                    //    else if (action.ActionMethod.ReturnType == typeof(Task))
                    //    {
                    //        action.Filters.Add(new ProducesResponseTypeAttribute(action.ActionMethod.ReturnType, StatusCodes.Status200OK));
                    //    }
                    //    else
                    //    {
                    //        action.Filters.Add(new ProducesResponseTypeAttribute(action.ActionMethod.ReturnType, StatusCodes.Status200OK));
                    //    }
                    //}
                }
            }
        }
    }
}
