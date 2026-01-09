
using FiscalFlow.Application.DTOs.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICfdiXsdValidator
{
    IReadOnlyCollection<CfdiErrorDetailDto> Validate(XDocument xml);
}

