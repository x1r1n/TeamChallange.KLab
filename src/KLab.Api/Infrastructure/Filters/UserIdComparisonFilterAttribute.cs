using KLab.Domain.Core.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace KLab.Api.Infrastructure.Filters
{
	public class UserIdComparisonFilterAttribute : Attribute, IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var requestId = context.HttpContext.Request.RouteValues["id"] as string;
			var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (requestId != userId)
			{
				context.Result = new JsonResult(DomainErrors.Request.InappropriateUserIdentifier)
				{
					StatusCode = StatusCodes.Status403Forbidden
				};

				return;
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}
