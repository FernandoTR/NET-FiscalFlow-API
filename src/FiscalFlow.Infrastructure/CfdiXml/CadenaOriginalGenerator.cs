using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Logging;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace FiscalFlow.Infrastructure.CfdiXml;

public sealed class CadenaOriginalGenerator : ICadenaOriginalGenerator
{
    private readonly ILogService _logService;
    public CadenaOriginalGenerator(ILogService logService)
    {
        _logService = logService;
    }
    public CfdiSuccessResponseDto<string> Generate(XDocument cfdiXml)
    {
        try
        {
            var xslt = LoadXslt();

            using var xmlReader = cfdiXml.CreateReader();
            using var stringWriter = new StringWriterWithEncoding(Encoding.UTF8);
            using var xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings);

            xslt.Transform(xmlReader, xmlWriter);
            string cadenaOriginal = Normalize(stringWriter.ToString());

            return new CfdiSuccessResponseDto<string>
            {
                IsSuccess = true,
                Message = "La cadena original fue creada correctamente.",
                Data = cadenaOriginal
            };
        }
        catch (Exception ex)
        {
            _logService.ErrorLog("CadenaOriginalGenerator.Generate", ex);

            return new CfdiSuccessResponseDto<string>
            {
                IsSuccess = false,
                Message = "Error al generar la cadena original: " + ex.Message
            };
        }      
       

    }

    private XslCompiledTransform LoadXslt()
    {
        var assembly = typeof(CadenaOriginalGenerator).Assembly;

        using var stream = assembly.GetManifestResourceStream("FiscalFlow.Infrastructure.CfdiXml.Schemas.Cfdi40.cadenaoriginal_4_0.xslt");

        if (stream is null)
        {
            _logService.ErrorLog("FiscalFlow.Infrastructure.CadenaOriginalGenerator", "LoadXslt", $"No se pudo cargar el XSD embebido: cadenaoriginal_4_0.xslt");
            throw new InvalidOperationException("No se pudo cargar el XSLT oficial del SAT.");
        }
           

        using var reader = XmlReader.Create(stream);

        var xslt = new XslCompiledTransform();
        xslt.Load(reader);

        return xslt;
    }

    private static string Normalize(string cadena)
    {
        // El SAT exige:
        // - Sin saltos de línea
        // - Sin espacios extra
        // - Texto plano
        return cadena
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty)
            .Trim();
    }

    internal sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding => _encoding;
    }

}