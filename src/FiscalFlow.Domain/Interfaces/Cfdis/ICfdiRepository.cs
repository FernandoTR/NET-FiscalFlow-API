using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Cfdis;

public interface ICfdiRepository
{
    Task<Cfdi?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Cfdi cfdi);
    Task<bool> UpdateAsync(Cfdi cfdi);
}

