
namespace FiscalFlow.Application.DTOs.Batches;

public record BatchStatusDto(
    Guid BatchId,
    string Status,
    int TotalItems,
    int ProcessedItems
);

