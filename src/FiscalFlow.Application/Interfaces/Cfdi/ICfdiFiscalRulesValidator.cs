using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICfdiFiscalRulesValidator
{
    Task<IReadOnlyList<CfdiErrorDetailDto>> ValidateAsync(CreateCfdiRequestDto request, CancellationToken ct);
}

