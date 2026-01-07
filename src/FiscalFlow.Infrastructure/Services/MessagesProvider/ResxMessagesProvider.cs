using FiscalFlow.Application.Interfaces.Logging;
using FiscalFlow.Application.Interfaces.Message;
using System.Reflection;
using System.Resources;

namespace FiscalFlow.Infrastructure.Services.MessagesProvider;

public class ResxMessagesProvider : IMessagesProvider
{
    private static readonly ResourceManager ErrorMessageResourceManager =
         new ResourceManager("FiscalFlow.Infrastructure.Resources.ErrorMessage", Assembly.GetExecutingAssembly());

    private static readonly ResourceManager MessageResourceManager =
        new ResourceManager("FiscalFlow.Infrastructure.Resources.Message", Assembly.GetExecutingAssembly());

    private readonly ILogService _logService;

    public ResxMessagesProvider(ILogService logService) { _logService = logService; }



    public string GetError(string resourceName)
    {
        try
        {
            // Acceder a los recursos de error
            return ErrorMessageResourceManager.GetString(resourceName);
        }
        catch (Exception ex)
        {
            // Manejo de errores
            _logService.ErrorLog("ResourceService.GetError", ex);
            return string.Empty;
        }
    }

    public string GetMessage(string resourceName)
    {
        try
        {
            // Acceder a los recursos de mensaje
            return MessageResourceManager.GetString(resourceName);
        }
        catch (Exception ex)
        {
            // Manejo de errores
            _logService.ErrorLog("ResourceService.GetMessage", ex);
            return string.Empty;
        }
    }
}
