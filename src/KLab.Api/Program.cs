using FluentValidation.AspNetCore;
using KLab.Api.Infrastructure.Authentication;
using KLab.Api.Infrastructure.Configurations;
using KLab.Api.Infrastructure.Handlers;
using KLab.Application;
using KLab.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddTransient<ApiKeyMiddleware>();

builder.Services
	.AddApplication()
	.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<FormOptions>(options =>
{
	options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
});

builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGen(options => options.ConfigureSwaggerGen());

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

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