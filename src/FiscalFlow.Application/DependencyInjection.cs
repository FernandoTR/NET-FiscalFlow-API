using Microsoft.Extensions.Hosting;

namespace FiscalFlow.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Registrar el servicio de mensajes de validación
        //builder.Services.AddTransient<IMessageService, MessageService>();


        //builder.Services.AddScoped<IActivityLogService, ActivityLogService>();






    }
}
