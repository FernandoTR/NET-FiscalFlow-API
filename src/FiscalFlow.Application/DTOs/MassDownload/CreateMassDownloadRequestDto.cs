
namespace FiscalFlow.Application.DTOs.MassDownload;

public record CreateMassDownloadRequestDto(
    Guid UserId,
    DateTime StartDate,
    DateTime EndDate
);

