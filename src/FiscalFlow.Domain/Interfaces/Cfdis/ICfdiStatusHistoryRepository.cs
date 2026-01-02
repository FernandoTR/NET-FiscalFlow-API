
namespace FiscalFlow.Domain.Interfaces.Cfdis;

public interface ICfdiStatusHistoryRepository
{
    Task AddAsync(CfdiStatusHistory history);
}
