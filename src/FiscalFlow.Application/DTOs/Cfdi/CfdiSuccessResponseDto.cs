namespace FiscalFlow.Application.DTOs.Cfdi;

public class CfdiSuccessResponseDto<T> : CfdiResponseDto
{
    public T Data { get; init; }
}
