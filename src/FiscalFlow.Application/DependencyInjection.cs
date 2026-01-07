using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Message;
using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Application.Interfaces.Validations;
using FiscalFlow.Application.Services.SatCatalog;
using FiscalFlow.Application.Validations.CfdiFiscalRules;
using FiscalFlow.Application.Validators.Cfdi;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;


namespace FiscalFlow.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Registrar TODOS los validators del Application layer
        builder.Services.AddValidatorsFromAssembly(
            Assembly.Load("FiscalFlow.Application")
        );      
        

        // Registrar casos de uso
        builder.Services.AddScoped<ICreateCfdiUseCase, CreateCfdiUseCase>();
        builder.Services.AddScoped<ISatCatalogService, SatCatalogService>();
        builder.Services.AddScoped<SatCatalogService>();
        builder.Services.AddScoped<ICfdiFiscalRulesValidator, CfdiFiscalRulesValidator>();
       



    }
}
