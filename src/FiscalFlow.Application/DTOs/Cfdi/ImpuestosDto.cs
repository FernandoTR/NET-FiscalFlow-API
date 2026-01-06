
namespace FiscalFlow.Application.DTOs.Cfdi;

public class ImpuestosDto
{
    public string TotalImpuestosTrasladados { get; set; }
    public string TotalImpuestosRetenidos { get; set; }

    public List<RetencionGlobalDto> Retenciones { get; set; }
    public List<TrasladoGlobalDto> Traslados { get; set; }
}

