using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Message;
using FiscalFlow.Application.Interfaces.Validations;
using System.Xml.Linq;

namespace FiscalFlow.Application.Services.Cfdi;

public sealed class CreateAndStampCfdiService : ICreateAndStampCfdiService
{

    private readonly IMessagesProvider _messagesProvider;
    private readonly ICreateCfdiUseCase _createCfdiUseCase;
    private readonly ICfdiFiscalRulesValidator _fiscalRulesValidator;
    private readonly ICfdiTotalsValidator _cfdiTotalsValidator;
    private readonly ICreateCfdiService _createCfdiService;
    private readonly ICfdiXmlBuilder _xmlBuilder;
    private readonly ICfdiValidateXmlStructure _cfdiValidateXmlStructure;
    private readonly ICadenaOriginalGenerator _cadenaOriginalGenerator;
    private readonly ICfdiXsdValidator _xsdValidator;

    public CreateAndStampCfdiService(IMessagesProvider messagesProvider,
                                    ICreateCfdiUseCase createCfdiUseCase,
                                    ICfdiFiscalRulesValidator fiscalRulesValidator,
                                    ICfdiTotalsValidator cfdiTotalsValidator,
                                    ICreateCfdiService createCfdiService,
                                    ICfdiXmlBuilder xmlBuilder,
                                    ICfdiValidateXmlStructure cfdiValidateXmlStructure,
                                    ICadenaOriginalGenerator cadenaOriginalGenerator,
                                    ICfdiXsdValidator xsdValidator)
    {
        _messagesProvider = messagesProvider;
        _createCfdiUseCase = createCfdiUseCase;
        _fiscalRulesValidator = fiscalRulesValidator;
        _cfdiTotalsValidator = cfdiTotalsValidator;
        _createCfdiService = createCfdiService;
        _xmlBuilder = xmlBuilder;
        _cfdiValidateXmlStructure = cfdiValidateXmlStructure;
        _cadenaOriginalGenerator = cadenaOriginalGenerator;
        _xsdValidator = xsdValidator;
    }

    public async Task<CfdiResponseDto> ExecuteAsync(CreateCfdiRequestDto request, CancellationToken cancellationToken)
    {
        #region Fase 1 – Validaciones internas
        // 1.1 Validar JSON / DTO (FluentValidation)
        var createCfdiUseCaseResult = await _createCfdiUseCase.ExecuteAsync(request, cancellationToken);
        if(!createCfdiUseCaseResult.IsSuccess)
            return createCfdiUseCaseResult;

        // 1.2 Validar reglas fiscales y catálogos SAT
        var fiscalRulesValidatorResult = await _fiscalRulesValidator.ValidateAsync(request, cancellationToken);
        if (fiscalRulesValidatorResult.Any())
            return new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = _messagesProvider.GetError("FiscalValidationFailed"),
                Errors = fiscalRulesValidatorResult
            };

        // 1.3 Calcular importes y totales
        var cfdiTotalsValidatorResult = _cfdiTotalsValidator.Validate(request);
        if (cfdiTotalsValidatorResult.Any())
            return new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = _messagesProvider.GetError("TotalsValidationFailed"),
                Errors = cfdiTotalsValidatorResult
            };

        // 1.4 Construir XML (sin sello)
        var _createCfdiServiceResult = _createCfdiService.Execute(request);
        if (!_createCfdiServiceResult.IsSuccess)
            return _createCfdiServiceResult;

        var xml = ((CfdiSuccessResponseDto<XDocument>)_createCfdiServiceResult).Data;


        // 1.5 Validar estructura XML (antes de sellar)
        var validateXmlStructureResult = _cfdiValidateXmlStructure.Execute(xml);
        if (validateXmlStructureResult.Any())
            return new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = "Errores al generar el XML CFDI.",
                Errors = validateXmlStructureResult
            };


        #endregion

        #region Fase 2 – Cadena original
        var cadenaOriginalGeneratorResult = _cadenaOriginalGenerator.Generate(xml);
        if (!cadenaOriginalGeneratorResult.IsSuccess)
        {
            return new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = "Error al generar la Cadena Original.",
                Errors = new List<CfdiErrorDetailDto>()
                {
                    new CfdiErrorDetailDto
                    {
                        Field = "Cadena Original",
                        Message = cadenaOriginalGeneratorResult.Message
                    }
                }
            };
        }     
     
        #endregion

        #region Fase 3 – Sellado digital (Aquí el XML queda completo)
        // 3.1 Firmar la cadena original con el CSD
        //var selloResult = _cfdiSigner.Sign(xml, cadenaResult.Data);
        // 3.2 Insertar el sello digital en el XML
        #endregion

        #region Fase 4 – Validación XSD 
        var xsdValidatorResult = _xsdValidator.Validate(xml);
        if (xsdValidatorResult.Any())
            return new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = _messagesProvider.GetError("SchemaValidationFailed"),
                Errors = xsdValidatorResult
            };
        #endregion

        #region Fase 5 - Timbrado PAC
        //return await _pacTimbradoService.TimbrarAsync(xml, cancellationToken);
        #endregion


        return new CfdiSuccessResponseDto<XDocument>
        {
            IsSuccess = true,
            Message = "XML CFDI generado correctamente.",
            Data = xml
        };

    }
}

