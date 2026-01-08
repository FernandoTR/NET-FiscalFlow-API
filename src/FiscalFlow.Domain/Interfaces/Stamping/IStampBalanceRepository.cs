using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Stamping;

public interface IStampBalanceRepository
{
    Task<StampBalance?> GetByUserIdAsync(Guid userId);
    Task UpdateAsync(StampBalance balance);
}

