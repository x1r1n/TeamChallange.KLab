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
	}
}