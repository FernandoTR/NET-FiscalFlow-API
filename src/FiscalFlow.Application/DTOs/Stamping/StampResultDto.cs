
namespace FiscalFlow.Application.DTOs.Stamping;

public record StampResultDto(
    Guid CfdiId,
    string Uuid,
    DateTime StampedAt
);

