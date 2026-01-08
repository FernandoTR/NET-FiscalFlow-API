using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Batches;

public interface IBatchItemRepository
{
    Task AddRangeAsync(IEnumerable<BatchItem> items);
}

