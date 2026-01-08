using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Certificates;

public interface ICertificateRepository
{
    Task<Certificate?> GetActiveByUserIdAsync(Guid userId);
    Task<Certificate?> GetByIdAsync(Guid certificateId);
    Task<bool> AddAsync(Certificate certificate);
    Task<bool> UpdateAsync(Certificate certificate);
}

