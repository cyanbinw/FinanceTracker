using FinanceTracker.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinanceTracker.Service.Filter
{
    public class ModelValidationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                BadResponse badRequest = new BadResponse();

                foreach (var item in context.ModelState)
                {
                    badRequest.Errors[item.Key] = item.Value.Errors.Select(c => c.ErrorMessage).ToArray();
                }

                badRequest.Message = "Bad Request";

                context.Result = new BadRequestObjectResult(badRequest);
            }

        }
    }
}
