using FiscalFlow.Application.Validations.CfdiFiscalRules;
using FiscalFlow.UnitTests.Builders;

namespace FiscalFlow.UnitTests.Cfdi.Totals;

public class CfdiTotalsValidatorTests
{
    private readonly CfdiTotalsValidator _validator = new();

    [Fact(DisplayName = "CFDI válido - Totales correctos")]
    public void Validate_ShouldPass_WhenCfdiAreCorrect()
    {
        // Arrange
        var cfdi = CfdiRequestBuilder.CreateValidCfdi();

        // Act
        var errors = _validator.Validate(cfdi);

        // Assert
        Assert.Empty(errors);
    }

    [Fact(DisplayName = "Error cuando SubTotal no coincide con conceptos")]
    public void Validate_ShouldFail_WhenSubtotalIsIncorrect()
    {
        // Arrange
        var cfdi = CfdiRequestBuilder.CreateValidCfdi();
        cfdi.Comprobante.SubTotal = 300.00m; // Incorrecto

        // Act
        var errors = _validator.Validate(cfdi);

        // Assert
        Assert.Contains(errors, e => e.Field == "Comprobante.SubTotal");
    }

    [Fact(DisplayName = "Error cuando Descuento no cuadra")]
    public void Validate_ShouldFail_WhenDiscountIsIncorrect()
    {
        // Arrange
        var request = CfdiRequestBuilder.CreateValidCfdi();
        request.Comprobante.Descuento = 50.00m; // Incorrecto

        // Act
        var errors = _validator.Validate(request);

        // Assert
        Assert.Contains(errors, e => e.Field == "Comprobante.Descuento");
    }

    [Fact(DisplayName = "Error cuando TotalImpuestosTrasladados no cuadra")]
    public void Validate_ShouldFail_WhenTrasladosAreIncorrect()
    {
        // Arrange
        var cfdi = CfdiRequestBuilder.CreateValidCfdi();
        cfdi.Impuestos.TotalImpuestosTrasladados = 40.00m; // Incorrecto

        // Act
        var errors = _validator.Validate(cfdi);

        // Assert
        Assert.Contains(errors, e => e.Field == "Impuestos.TotalImpuestosTrasladados");
    }

    [Fact(DisplayName = "Error cuando Total final no coincide")]
    public void Validate_ShouldFail_WhenTotalIsIncorrect()
    {
        // Arrange
        var cfdi = CfdiRequestBuilder.CreateValidCfdi();
        cfdi.Comprobante.Total = 120.00m;

        // Act
        var errors = _validator.Validate(cfdi);

        // Assert
        Assert.Contains(errors, e => e.Field == "Comprobante.Total");
    }

    [Fact(DisplayName = "Redondeo SAT AwayFromZero (caso real)")]
    public void Validate_ShouldApplyAwayFromZeroRounding()
    {
        // Arrange
        var cfdi = CfdiRequestBuilder.CreateValidCfdi();

        cfdi.Conceptos[0].ValorUnitario = 333.333m;
        cfdi.Conceptos[0].Cantidad = 3;

        // 333.333 * 3 = 999.999 → 1000.00
        cfdi.Comprobante.SubTotal = 1000.00m;
        cfdi.Impuestos.TotalImpuestosTrasladados = 160.00m;
        cfdi.Comprobante.Total = 1160.00m;

        cfdi.Conceptos[0].Impuestos.Traslados[0].Base = 1000.00m;
        cfdi.Conceptos[0].Impuestos.Traslados[0].Importe = 160.00m;

        // Act
        var errors = _validator.Validate(cfdi);

        // Assert
        Assert.True(errors.Any());
    }
}

