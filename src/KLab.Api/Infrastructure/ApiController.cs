using KLab.Domain.Core.Primitives.ErrorModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLab.Api.Infrastructure
{
	/// <summary>
	/// Represents the base controller
	/// </summary>
	[Authorize]
	[ApiController]
	[Route("api")]
	public class ApiController : ControllerBase
	{
		public readonly IMediator _mediator;

		protected ApiController(IMediator mediator) => _mediator = mediator;

		/// <summary>
		/// Handles failures based on the provided errors and returns the corresponding status code
		/// </summary>
		/// <param name="errors">The list of errors to handle</param>
		/// <returns>A response with the status code and error list.</returns>
		protected IActionResult HandleFailure(IEnumerable<Error> errors) =>
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