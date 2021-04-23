using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Inventory.Web.Extensions
{
    public class ModelStateValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
                actionContext.Response = actionContext.Request
                     .CreateErrorResponse(HttpStatusCode.BadRequest, modelState.Values.SelectMany(v => v.Errors).ToString());
        }
    }
}
