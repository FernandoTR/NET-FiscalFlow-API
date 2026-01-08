
using System.Xml.Linq;

namespace FiscalFlow.Application.Interfaces.Cfdi;

public interface ICfdiXsdValidator
{
    void Validate(XDocument xml);
}

