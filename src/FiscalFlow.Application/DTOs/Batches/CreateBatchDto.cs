
namespace FiscalFlow.Application.DTOs.Batches;

public record CreateBatchDto(
    Guid UserId,
    IEnumerable<Guid> CfdiIds
);

