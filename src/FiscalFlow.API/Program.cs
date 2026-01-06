using Asp.Versioning;
using FiscalFlow.Application;
using FiscalFlow.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplicationServices();
builder.AddInfrastructureServices();

// Controllers
builder.Services.AddControllers();

// Versioning 
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>  
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FiscalFlow API v1", Version = "v1" });
    //options.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2", Version = "v2" });
});

// Authentication / Authorization
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "FiscalFlow.API",
            ValidAudience = "FiscalFlow.Client",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("CAMBIA_ESTA_CLAVE_LARGA_Y_SEGURA")
            )
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        //c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
    });
}

// Global exception handling for validation errors (FluentValidation)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features
            .Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationEx)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                errors = validationEx.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                })
            });
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();