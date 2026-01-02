
namespace FiscalFlow.Domain.Interfaces.Cfdis;

public interface ICfdiPdfRepository
{
    Task AddAsync(CfdiPdf pdf);
}
