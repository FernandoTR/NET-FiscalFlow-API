using FiscalFlow.Application.DTOs.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICfdiXmlBuilder
{
    XDocument Build(CreateCfdiRequestDto cfdi);
}

