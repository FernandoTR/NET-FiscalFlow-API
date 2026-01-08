using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Batches;

public interface ICfdiBatchRepository
{
    Task<CfdiBatch?> GetByIdAsync(Guid id);
    Task AddAsync(CfdiBatch batch);
    void Update(CfdiBatch batch);
}
