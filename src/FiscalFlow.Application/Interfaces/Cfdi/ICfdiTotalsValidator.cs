using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICfdiTotalsValidator
{
    IReadOnlyList<CfdiErrorDetailDto> Validate(CreateCfdiRequestDto cfdi);
}
