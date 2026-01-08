using FiscalFlow.Application.Interfaces.Auth;
using FiscalFlow.Application.Interfaces.Caching;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Logging;
using FiscalFlow.Application.Interfaces.Message;
using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Domain.Interfaces.Auth;
using FiscalFlow.Domain.Interfaces.Common;
using FiscalFlow.Domain.Interfaces.SatCatalog;
using FiscalFlow.Domain.Interfaces.Users;
using FiscalFlow.Infrastructure.CfdiXml;
using FiscalFlow.Infrastructure.Logging;
using FiscalFlow.Infrastructure.Persistence.Data;
using FiscalFlow.Infrastructure.Persistence.Repositories;
using FiscalFlow.Infrastructure.Security;
using FiscalFlow.Infrastructure.Services.Caching;
using FiscalFlow.Infrastructure.Services.MessagesProvider;
using FiscalFlow.Infrastructure.Services.SatCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FiscalFlow.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        // Infrastructure service registrations would go here

        // DataBase
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddDbContext<LoggingDbContext>(options => options.UseSqlServer(connectionString));

        // Repository
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ILogService, LogService>();
        builder.Services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
        builder.Services.AddScoped<ISatCatalogRepository, SatCatalogRepository>();

        // Security
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // ResxMessagesProvider
        builder.Services.AddTransient<IMessagesProvider, ResxMessagesProvider>();

        // 
        builder.Services.AddScoped<ISatCatalogWarmupService, SatCatalogWarmupService>();
        builder.Services.AddScoped<ISatCatalogService, SatCatalogService>();
        builder.Services.AddScoped<SatCatalogService>();
        builder.Services.AddScoped<ICfdiXmlBuilder, CfdiXmlBuilder>();
        builder.Services.AddScoped<ICfdiXsdValidator, CfdiXsdValidator>();

        

    }


}
