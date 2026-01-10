
using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICreateCfdiService
{
    CfdiResponseDto Execute(CreateCfdiRequestDto request);
}
