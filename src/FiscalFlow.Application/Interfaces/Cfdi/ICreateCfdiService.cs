
using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICreateCfdiService
{
    Task<CfdiResponseDto> Execute(CreateCfdiRequestDto request);
}
