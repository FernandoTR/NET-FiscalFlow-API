namespace FiscalFlow.Application.DTOs.Cfdi;

public class TimbradoResultDto
{
    public string Uuid { get; init; }
    public string Status { get; init; } // VIGENTE, CANCELADO
    public string XmlBase64 { get; init; }
}
