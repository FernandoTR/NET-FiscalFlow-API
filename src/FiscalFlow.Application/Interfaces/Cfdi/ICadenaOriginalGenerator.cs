using FiscalFlow.Application.DTOs.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICadenaOriginalGenerator
{
    CfdiSuccessResponseDto<string> Generate(XDocument cfdiXml);
}

