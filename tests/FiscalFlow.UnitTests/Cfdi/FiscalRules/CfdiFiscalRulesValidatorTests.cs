using FiscalFlow.Application.Interfaces.Message;
using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Application.Validations.CfdiFiscalRules;
using FiscalFlow.UnitTests.Builders;
using Moq;

namespace FiscalFlow.UnitTests.Cfdi.FiscalRules;

public class CfdiFiscalRulesValidatorTests
{
    private readonly Mock<ISatCatalogService> _catalogMock = new();
    private readonly Mock<IMessagesProvider> _messagesMock = new();
    private readonly CfdiFiscalRulesValidator _validator;

    public CfdiFiscalRulesValidatorTests()
    {
        _messagesMock
            .Setup(m => m.GetError(It.IsAny<string>()))
            .Returns("{0} no existe en el catálogo {1}");

        _validator = new CfdiFiscalRulesValidator(
            _catalogMock.Object,
            _messagesMock.Object);
    }

    [Fact(DisplayName = "CFDI válido - Todos los catálogos existen")]
    public async Task ValidateAsync_ValidCfdi_ShouldPass()
    {
        // Arrange
        var request = CfdiRequestBuilder.CreateValidCfdi();

        _catalogMock
            .Setup(c => c.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _catalogMock
            .Setup(c => c.IsCombinationAllowedAsync(
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var errors = await _validator.ValidateAsync(request, CancellationToken.None);

        // Assert
        Assert.Empty(errors);
    }

    [Fact(DisplayName = "FormaPago inexistente - c_FormaPago inválido")]
    public async Task ValidateAsync_FormaPagoDoesNotExist_ShouldReturnFormaPagoError()
    {
        // Arrange
        var request = CfdiRequestBuilder.CreateValidCfdi();

        _catalogMock
            .Setup(c => c.ExistsAsync("c_FormaPago", request.Comprobante.FormaPago, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _catalogMock
            .Setup(c => c.ExistsAsync(It.IsNotIn("c_FormaPago"), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _catalogMock
            .Setup(c => c.IsCombinationAllowedAsync(It.IsAny<string>(), It.IsAny<string>(),
                                                    It.IsAny<string>(), It.IsAny<string>(),
                                                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var errors = await _validator.ValidateAsync(request, CancellationToken.None);

        // Assert
        Assert.Contains(errors, e => e.Field == "Comprobante.FormaPago");
    }

    [Fact(DisplayName = "FormaPago + MétodoPago inválidos - Combinación no permitida")]
    public async Task ValidateAsync_FormaPagoMetodoPagoInvalidCombination_ShouldReturnCombinationError()
    {
        // Arrange
        var request = CfdiRequestBuilder.CreateValidCfdi();

        _catalogMock
            .Setup(c => c.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _catalogMock
            .Setup(c => c.IsCombinationAllowedAsync(
                "c_FormaPago", request.Comprobante.FormaPago,
                "c_MetodoPago", request.Comprobante.MetodoPago,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var errors = await _validator.ValidateAsync(request, CancellationToken.None);

        // Assert
        Assert.Contains(errors, e => e.Field == "Comprobante.MetodoPago");
    }

    [Fact(DisplayName = "ClaveProdServ inválida - Conceptos[i]")]
    public async Task ValidateAsync_ClaveProdServDoesNotExist_ShouldReturnConceptError()
    {
        // Arrange
        var request = CfdiRequestBuilder.CreateValidCfdi();

        _catalogMock
            .Setup(c => c.ExistsAsync("c_ClaveProdServ", It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _catalogMock
            .Setup(c => c.ExistsAsync(It.IsNotIn("c_ClaveProdServ"), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _catalogMock
            .Setup(c => c.IsCombinationAllowedAsync(It.IsAny<string>(), It.IsAny<string>(),
                                                    It.IsAny<string>(), It.IsAny<string>(),
                                                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var errors = await _validator.ValidateAsync(request, CancellationToken.None);

        // Assert
        Assert.Contains(errors, e => e.Field == "Conceptos[0].ClaveProdServ");
    }

    [Fact(DisplayName = "Impuesto inválido - c_Impuesto")]
    public async Task ValidateAsync_ImpuestoDoesNotExist_ShouldReturnImpuestoError()
    {
        // Arrange
        var request = CfdiRequestBuilder.CreateValidCfdi();

        _catalogMock
            .Setup(c => c.ExistsAsync("c_Impuesto", It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _catalogMock
            .Setup(c => c.ExistsAsync(It.IsNotIn("c_Impuesto"), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _catalogMock
            .Setup(c => c.IsCombinationAllowedAsync(It.IsAny<string>(), It.IsAny<string>(),
                                                    It.IsAny<string>(), It.IsAny<string>(),
                                                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var errors = await _validator.ValidateAsync(request, CancellationToken.None);

        // Assert
        Assert.Contains(errors, e => e.Field == "Impuestos.Traslados.Impuesto");
    }
}
