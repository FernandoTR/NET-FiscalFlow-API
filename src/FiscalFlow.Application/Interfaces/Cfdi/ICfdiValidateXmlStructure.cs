using FiscalFlow.Application.DTOs.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICfdiValidateXmlStructure
{
    IReadOnlyCollection<CfdiErrorDetailDto> Execute(XDocument xml);
}
