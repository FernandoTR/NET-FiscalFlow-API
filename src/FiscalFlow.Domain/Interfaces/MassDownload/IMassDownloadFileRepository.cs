using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.MassDownload;

public interface IMassDownloadFileRepository
{
    Task AddAsync(MassDownloadFile file);
}
