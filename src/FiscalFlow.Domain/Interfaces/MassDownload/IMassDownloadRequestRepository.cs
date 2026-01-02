
namespace FiscalFlow.Domain.Interfaces.MassDownload;

public interface IMassDownloadRequestRepository
{
    Task<MassDownloadRequest?> GetByIdAsync(Guid id);
    Task AddAsync(MassDownloadRequest request);
    void Update(MassDownloadRequest request);
}
