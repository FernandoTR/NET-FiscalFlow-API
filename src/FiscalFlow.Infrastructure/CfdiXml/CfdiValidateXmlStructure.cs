using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Infrastructure.CfdiXml;

public class CfdiValidateXmlStructure : ICfdiValidateXmlStructure
{
    public IReadOnlyCollection<CfdiErrorDetailDto> Execute(XDocument xml)
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

}
