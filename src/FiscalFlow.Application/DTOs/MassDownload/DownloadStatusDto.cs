
namespace FiscalFlow.Application.DTOs.MassDownload;

public record DownloadStatusDto(
    Guid RequestId,
    string Status,
    int TotalFiles,
    int DownloadedFiles
);
