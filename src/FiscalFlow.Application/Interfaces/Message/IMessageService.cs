
namespace FiscalFlow.Application.Interfaces.Message;

public interface IMessageService
{
    string GetResourceError(string resourceName);
    string GetResourceMessage(string resourceName);
}
