using FluentValidation;
using KLab.Application.Core.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KLab.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddValidatorsFromAssembly(assembly);

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

			return services;
		}
	}
}