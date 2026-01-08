using FiscalFlow.Application.DTOs.Cfdi;

namespace FiscalFlow.UnitTests.Builders;

public static class CfdiRequestBuilder
{
    public static CreateCfdiRequestDto CreateValidCfdi()
    {
        return new CreateCfdiRequestDto
        {
            Comprobante = new()
            {
                SubTotal = 350.00m,
                Descuento = 0.00m,
                Total = 406.00m
            },
            Conceptos =
            [
                new()
                {
                    Cantidad = 2,
                    ValorUnitario = 100.00m,
                    Descuento = 0.00m,
                    Impuestos = new()
                    {
                        Traslados =
                        [
                            new()
                            {
                                Importe = 32.00m // IVA 16%
                            }
                        ]
                    }
                },
                new()
                {
                    Cantidad = 1,
                    ValorUnitario = 150.00m,
                    Descuento = 0.00m,
                    Impuestos = new()
                    {
                        Traslados =
                        [
                            new()
                            {
                                Importe = 24.00m // IVA 16%
                            }
                        ]
                    }
                }
            ],
            Impuestos = new()
            {
                TotalImpuestosTrasladados = 56.00m,
                TotalImpuestosRetenidos = 0.00m
            }
        };
    }
}


