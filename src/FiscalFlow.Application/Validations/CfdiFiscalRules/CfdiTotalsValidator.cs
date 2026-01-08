using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;

namespace FiscalFlow.Application.Validations.CfdiFiscalRules;

public sealed class CfdiTotalsValidator : ICfdiTotalsValidator
{
    public IReadOnlyList<CfdiErrorDetailDto> Validate(CreateCfdiRequestDto cfdi)
    {
        var errors = new List<CfdiErrorDetailDto>();

        decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

        // 1️ SubTotal
        var subtotalCalculated = cfdi.Conceptos.Sum(c => Round(c.Cantidad * c.ValorUnitario));

        if (Round(cfdi.Comprobante.SubTotal) != Round(subtotalCalculated))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.SubTotal",
                Message = $"SubTotal incorrecto. Esperado: {subtotalCalculated:0.00}"
            });            
        }

        // 2️ Descuento
        var descuentoCalculated = cfdi.Conceptos.Where(c => c.Descuento > 0).Sum(c => c.Descuento) ?? 0;

        if (Round(cfdi.Comprobante.Descuento) != Round(descuentoCalculated))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.Descuento",
                Message = $"Descuento incorrecto. Esperado: {descuentoCalculated:0.00}"
            });            
        }

        // 3️ Impuestos trasladados
        var trasladosCalculated = cfdi.Conceptos.SelectMany(c => c.Impuestos?.Traslados ?? []).Sum(t => t.Importe);

        if (Round(cfdi.Impuestos.TotalImpuestosTrasladados) != Round(trasladosCalculated))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Impuestos.TotalImpuestosTrasladados",
                Message = $"Traslados incorrectos. Esperado: {trasladosCalculated:0.00}"
            });
        }

        // 4️ Impuestos retenidos
        var retencionesCalculated = cfdi.Conceptos.SelectMany(c => c.Impuestos?.Retenciones ?? []).Sum(r => r.Importe);

        if (Round(cfdi.Impuestos.TotalImpuestosRetenidos) != Round(retencionesCalculated))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Impuestos.TotalImpuestosRetenidos",
                Message = $"Retenciones incorrectas. Esperado: {retencionesCalculated:0.00}"
            });            
        }

        // 5️ Total
        var totalCalculated = Round(cfdi.Comprobante.SubTotal - cfdi.Comprobante.Descuento) + Round(cfdi.Impuestos.TotalImpuestosTrasladados)
                            - Round(cfdi.Impuestos.TotalImpuestosRetenidos);

        if (Round(cfdi.Comprobante.Total) != Round(totalCalculated))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.Total",
                Message = $"Total incorrecto. Esperado: {totalCalculated:0.00}"
            });
        }

        return errors;
    }
}

