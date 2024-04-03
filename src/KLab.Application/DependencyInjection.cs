using FluentValidation;
using KLab.Application.Core.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KLab.Application
{
	/// <summary>
	/// Provides extension methods for setting up dependency injection
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// Adds application services to the specified <see cref="IServiceCollection"/>
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add the services</param>
		/// <returns>The modified <see cref="IServiceCollection"/></returns>
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