using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Logging;
using System.Xml.Linq;

namespace FiscalFlow.Application.Services.Cfdi;

public sealed class CreateCfdiService: ICreateCfdiService
{
    private readonly ICfdiXmlBuilder _xmlBuilder;
    private readonly ILogService _logService;

    public CreateCfdiService(ICfdiXmlBuilder xmlBuilder, ILogService logService)
    {
        _xmlBuilder = xmlBuilder;
        _logService = logService;
    }

    public CfdiResponseDto Execute(CreateCfdiRequestDto request)
    {
        try
        {
            // Generar el XML CFDI 4.0
            var xml = _xmlBuilder.Build(request);

            return new CfdiSuccessResponseDto<XDocument>
            {
                IsSuccess = true,
                Message = "XML CFDI generado correctamente.",
                Data = xml
            };
        }
        catch (Exception ex)
        {
            _logService.ErrorLog("FiscalFlow.Application.Services.Cfdi.CreateCfdiService.Execute", ex);
            return new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = $"Ocurrió un error al generar el XML CFDI. Detalle: {ex.Message}"
            };
        }         
    } 

   

}
