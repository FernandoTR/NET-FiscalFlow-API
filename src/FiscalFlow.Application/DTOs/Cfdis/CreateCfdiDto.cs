
namespace FiscalFlow.Application.DTOs.Cfdis;

public record CreateCfdiDto(
    Guid UserId,
    string Serie,
    string Folio,
    decimal Total,
    string Currency
);

