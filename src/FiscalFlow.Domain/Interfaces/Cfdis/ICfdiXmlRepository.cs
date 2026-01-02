
namespace FiscalFlow.Domain.Interfaces.Cfdis;

public interface ICfdiXmlRepository
{
    Task AddAsync(CfdiXml xml);
    Task<CfdiXml?> GetByCfdiIdAsync(Guid cfdiId);
}
