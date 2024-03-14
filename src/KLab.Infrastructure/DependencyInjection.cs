using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Emails;
using KLab.Domain.Entities;
using KLab.Infrastructure.Core.Abstractions;
using KLab.Infrastructure.Core.Provider;
using KLab.Infrastructure.Core.Services;
using KLab.Infrastructure.Core.Services.Email;
using KLab.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KLab.Infrastructure
{
    public static class DependencyInjection
	{
        public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			ISecretManager secretManager)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var connectionString = secretManager.GetSecretAsync("ProdConnection").Result;

			services.AddDbContext<ApplicationDbContext>(
				options =>
				{
					options.UseSqlServer(
						connectionString,
						sql => sql.MigrationsAssembly(assembly.FullName));
				});

			services.Configure<IdentityOptions>(
				options =>
				{
					options.Tokens.EmailConfirmationTokenProvider = nameof(CustomEmailTokenProvider);

					options.User.RequireUniqueEmail = true;

					options.SignIn.RequireConfirmedEmail = true;
				});

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddTokenProvider<CustomEmailTokenProvider>(
				nameof(CustomEmailTokenProvider));

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

			services.AddScoped<IIdentityService, IdentityService>();

			services.AddScoped<IEmailService, EmailService>();

			services.AddScoped<SmtpConfiguration>();

			return services;
		}
	}
}