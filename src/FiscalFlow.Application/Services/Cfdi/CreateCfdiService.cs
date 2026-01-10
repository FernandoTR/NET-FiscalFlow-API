using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using System.Xml.Linq;

namespace FiscalFlow.Application.Services.Cfdi;

public sealed class CreateCfdiService: ICreateCfdiService
{
    private readonly ICfdiXmlBuilder _xmlBuilder;
    private readonly ICfdiValidateXmlStructure _cfdiValidateXmlStructure;
    private readonly ICadenaOriginalGenerator _cadenaOriginalGenerator;
    private readonly ICfdiXsdValidator _xsdValidator;

    public CreateCfdiService(ICfdiXmlBuilder xmlBuilder, 
                             ICfdiValidateXmlStructure cfdiValidateXmlStructure,
                             ICadenaOriginalGenerator cadenaOriginalGenerator,
                             ICfdiXsdValidator cfdiXsdValidator)
    {
        _xmlBuilder = xmlBuilder;
        _cfdiValidateXmlStructure = cfdiValidateXmlStructure;
        _cadenaOriginalGenerator = cadenaOriginalGenerator;
        _xsdValidator = cfdiXsdValidator;

    }

    public Task<CfdiResponseDto> Execute(CreateCfdiRequestDto request)
    {
        // Generar el XML CFDI 4.0
        var xml = _xmlBuilder.Build(request);

        // Validar la estructura del XML CFDI 4.0 generado (Sin XSD)
        var validateXmlStructureResult = _cfdiValidateXmlStructure.Execute(xml);

        if (validateXmlStructureResult.Any())
        {
            return Task.FromResult<CfdiResponseDto>(new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = "Errores al generar el XML CFDI.",
                Errors = validateXmlStructureResult
            });
        }

        // Generar Cadena Original con XSLT SAT
        var cadenaOriginalGeneratorResult = _cadenaOriginalGenerator.Generate(xml);

        if (!cadenaOriginalGeneratorResult.IsSuccess)
        {
            var errors = new List<CfdiErrorDetailDto>(1)
            {
                new CfdiErrorDetailDto
                {
                    Field = "Cadena Original",
                    Message = cadenaOriginalGeneratorResult.Message
                }
            };

            return Task.FromResult<CfdiResponseDto>(new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = "Error al generar la Cadena Original.",
                Errors = errors
            });
        }

        


        // Validar el XML CFDI 4.0 generado contra el XSD oficial del SAT
        //var xsdErrors = _xsdValidator.Validate(xml);

        //if (xsdErrors.Any())
        //{
        //    return Task.FromResult<CfdiResponseDto>(new CfdiErrorResponseDto
        //    {
        //        IsSuccess = false,
        //        Message = "El XML CFDI no cumple con el esquema XSD 4.0 del SAT.",
        //        Errors = xsdErrors
        //    });
        //}


        return Task.FromResult<CfdiResponseDto>( new CfdiSuccessResponseDto<XDocument>
            {
                IsSuccess = true,
                Message = "XML CFDI generado correctamente.",
                Data = xml
            });
    } 

   

}
