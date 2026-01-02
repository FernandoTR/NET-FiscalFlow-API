
namespace FiscalFlow.Application.DTOs.Cfdis;

public record CfdiDto(
    Guid Id,
    string Serie,
    string Folio,
    decimal Total,
    string Status,
    DateTime CreatedAt
);

