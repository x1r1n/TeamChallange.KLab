using KLab.Domain.Core.Primitives.ErrorModel;
using KLab.Domain.Core.Primitives.ResultModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLab.Api.Infrastructure
{
	[Authorize]
	[ApiController]
	[Route("api")]
	public class ApiController : ControllerBase
	{
		public readonly IMediator _mediator;

		protected ApiController(IMediator mediator) => _mediator = mediator;

		protected IActionResult HandleBadRequest(IEnumerable<Error> errors) =>
			errors.FirstOrDefault()!.Type switch
			{
				ErrorType.Failure => BadRequest(errors),
				ErrorType.Validition => UnprocessableEntity(errors),
				ErrorType.NotFound => NotFound(errors),
				ErrorType.Conflict => Conflict(errors),
				_ => BadRequest(errors)
			};
			
	}
}