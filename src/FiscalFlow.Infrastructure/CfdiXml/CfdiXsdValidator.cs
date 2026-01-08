using FiscalFlow.Application.Interfaces.Cfdi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FiscalFlow.Infrastructure.CfdiXml;

public sealed class CfdiXsdValidator : ICfdiXsdValidator
{
    private const string CfdiNamespace = "http://www.sat.gob.mx/cfd/4";

    public void Validate(XDocument xml)
    {
        var schemas = LoadSchemas();

        var errors = new List<string>();

        xml.Validate(
            schemas,
            (sender, e) =>
            {
                errors.Add(e.Message);
            },
            true
        );

        if (errors.Any())
        {
            throw new CfdiXsdValidationException(errors);
        }
    }

    private static XmlSchemaSet LoadSchemas()
    {
        var schemaSet = new XmlSchemaSet();

        var assembly = typeof(CfdiXsdValidator).Assembly;

        using var stream = assembly.GetManifestResourceStream("FiscalFlow.Infrastructure.XmlSchemas.Cfdi40.cfdv40.xsd");

        if (stream is null)
            throw new InvalidOperationException("No se pudo cargar el XSD CFDI 4.0.");

        using var reader = XmlReader.Create(stream);

        schemaSet.Add(CfdiNamespace, reader);
        schemaSet.Compile();

        return schemaSet;
    }

    public sealed class CfdiXsdValidationException : Exception
{
    public IReadOnlyList<string> Errors { get; }

    public CfdiXsdValidationException(IEnumerable<string> errors) : base("El XML CFDI no cumple con el XSD 4.0 del SAT.")
    {
        Errors = errors.ToList().AsReadOnly();
    }
}

}
