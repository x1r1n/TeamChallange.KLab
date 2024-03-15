using KLab.Application.Core.Abstractions.Data;
using KLab.Application.Core.Abstractions.Emails;
using KLab.Domain.Entities;
using KLab.Infrastructure.Core.Provider;
using KLab.Infrastructure.Core.Services;
using KLab.Infrastructure.Core.Services.Email;
using KLab.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KLab.Infrastructure
{
    public static class DependencyInjection
	{
        public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddDbContext<ApplicationDbContext>(
				options =>
				{
					options.UseSqlServer(
						configuration.GetConnectionString("DefaultConnection"),
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