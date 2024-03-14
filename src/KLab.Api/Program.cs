using Serilog;
using KLab.Application;
using KLab.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using KLab.Api.Infrastructure.Handlers;
using KLab.Api.Infrastructure.Authentication;
using KLab.Api.Infrastructure.Configurations;
using KLab.Infrastructure.Core.Abstractions;
using KLab.Api.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddTransient<ApiKeyMiddleware>();

builder.Services.AddSingleton<ISecretManager, SecretManager>();

builder.Services
	.AddApplication()
	.AddInfrastructure(new SecretManager(builder.Configuration));

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "KLab API",
		Version = "v1"
	});

	options.AddApiKeySecurity();
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI(
	options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "KLab API v1");
		options.RoutePrefix = string.Empty;
	});

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();