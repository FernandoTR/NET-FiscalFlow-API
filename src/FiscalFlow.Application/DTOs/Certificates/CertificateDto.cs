
namespace FiscalFlow.Application.DTOs.Certificates;

public record CertificateDto(
    Guid Id,
    bool IsActive,
    DateTime CreatedAt
);

