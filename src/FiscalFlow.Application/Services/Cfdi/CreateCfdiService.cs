using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Application.Services.Cfdi;

public sealed class CreateCfdiService: ICreateCfdiService
{
    private readonly ICfdiXmlBuilder _xmlBuilder;

    public CreateCfdiService(ICfdiXmlBuilder xmlBuilder)
    {
        _xmlBuilder = xmlBuilder;
    }

    public Task<CfdiResponseDto> Execute(CreateCfdiRequestDto request)
    {
        var xml = _xmlBuilder.Build(request);

        var errors = ValidateXmlStructure(xml);


        if (errors.Any())
        {
            return Task.FromResult<CfdiResponseDto>( new CfdiErrorResponseDto
                {
                    IsSuccess = false,
                    Message = "Errores al generar el XML CFDI.",
                    Errors = errors
                });
        }

        return Task.FromResult<CfdiResponseDto>( new CfdiSuccessResponseDto<XDocument>
            {
                IsSuccess = true,
                Message = "XML CFDI generado correctamente.",
                Data = xml
            });
    }

    private static IReadOnlyCollection<CfdiErrorDetailDto> ValidateXmlStructure(XDocument xml)
    {
        var errors = new List<CfdiErrorDetailDto>();

        if (xml.Root is null)
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "cfdi:Comprobante",
                Message = "No se pudo generar el XML CFDI: documento sin nodo raíz."
            });

            return errors;
        }

        if (xml.Root.Name != CfdiNamespaces.Cfdi + "Comprobante")
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "cfdi:Comprobante",
                Message = "El XML generado no contiene el nodo cfdi:Comprobante."
            });

            return errors;
        }

        ValidateRequiredNode(xml.Root, "Emisor", errors);
        ValidateRequiredNode(xml.Root, "Receptor", errors);
        ValidateRequiredNode(xml.Root, "Conceptos", errors);

        return errors;
    }

    private static void ValidateRequiredNode(XElement comprobante, string nodeName, ICollection<CfdiErrorDetailDto> errors)
    {
        if (!comprobante.Elements(CfdiNamespaces.Cfdi + nodeName).Any())
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = nodeName,
                Message = $"CFDI sin nodo {nodeName}."
            });
        }
    }

    public static class CfdiNamespaces
    {
        public static readonly XNamespace Cfdi = "http://www.sat.gob.mx/cfd/4";
        public static readonly XNamespace Xsi = "http://www.w3.org/2001/XMLSchema-instance";
    }

}
