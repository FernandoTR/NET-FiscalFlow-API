
namespace FiscalFlow.Application.DTOs.Certificates;

public record UploadCertificateDto(
    Guid UserId,
    byte[] CerFile,
    byte[] KeyFile,
    string Password
);

