using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Logging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FiscalFlow.Infrastructure.CfdiXml;

public sealed class CfdiXsdValidator : ICfdiXsdValidator
{
    private const string CfdiNamespace = "http://www.sat.gob.mx/cfd/4";
    private readonly ILogService _logService;

    public CfdiXsdValidator(ILogService logService)
    {
        _logService = logService;
    }

    public IReadOnlyCollection<CfdiErrorDetailDto> Validate(XDocument xml)
    {
        var errors = new List<CfdiErrorDetailDto>();
        var schemas = LoadSchemas();

        xml.Validate(
            schemas,
            (sender, e) =>
            {
                errors.Add(new CfdiErrorDetailDto
                {
                    Field = ExtractField(e),
                    Message = e.Message
                });
            },
            true
        );

        return errors;
    }

    private XmlSchemaSet LoadSchemas()
    {
        var schemaSet = new XmlSchemaSet();

        try
        {
            var assembly = typeof(CfdiXsdValidator).Assembly;

            var xsdResources = new[]
            {
                "FiscalFlow.Infrastructure.CfdiXml.Schemas.Cfdi40.cfdv40.xsd",
                "FiscalFlow.Infrastructure.CfdiXml.Schemas.Cfdi40.tdCFDI.xsd",
                "FiscalFlow.Infrastructure.CfdiXml.Schemas.Cfdi40.catCFDI.xsd",
                "FiscalFlow.Infrastructure.CfdiXml.Schemas.TimbreFiscal.TimbreFiscalDigitalv11.xsd"
            };

            foreach (var resource in xsdResources)
            {
                using var stream = assembly.GetManifestResourceStream(resource);

                if (stream is null)
                {
                    _logService.ErrorLog("FiscalFlow.Infrastructure.CfdiXsdValidator", "LoadSchemas", $"No se pudo cargar el XSD embebido: {resource}");
                    //throw new InvalidOperationException($"No se pudo cargar el XSD embebido: {resource}");
                    break;
                }

                using var reader = XmlReader.Create(stream);
                schemaSet.Add(null, reader);
            }

            schemaSet.Compile();
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(LoadSchemas), ex);
        }       

        return schemaSet;
    }

    private static string ExtractField(ValidationEventArgs e)
    {
        if (e.Exception is XmlSchemaValidationException ex)
        {
            return !string.IsNullOrWhiteSpace(ex.SourceObject?.ToString())
                ? ex.SourceObject.ToString()!
                : "XML";
        }

        return "XML";
    }
}
