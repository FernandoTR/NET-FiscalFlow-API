
using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICreateAndStampCfdiService
{
    Task<CfdiResponseDto> ExecuteAsync(CreateCfdiRequestDto request, CancellationToken cancellationToken);
}
