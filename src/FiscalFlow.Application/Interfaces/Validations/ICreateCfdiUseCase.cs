
using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.Application.Interfaces.Validations;

public interface ICreateCfdiUseCase
{
    Task<CfdiResponseDto> ExecuteAsync(CreateCfdiRequestDto request, CancellationToken ct);
}
