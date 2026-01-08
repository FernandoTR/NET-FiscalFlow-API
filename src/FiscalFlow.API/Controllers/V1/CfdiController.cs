using Asp.Versioning;
using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.Message;
using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Application.Interfaces.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiscalFlow.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cfdi")]
[Authorize]
public class CfdiController : Controller
{
    private readonly IMessagesProvider _messagesProvider;
    private readonly ICreateCfdiUseCase _createCfdiUseCase;
    private readonly ICfdiFiscalRulesValidator _fiscalRulesValidator;
    private readonly ICfdiTotalsValidator _cfdiTotalsValidator;
    private readonly ICreateCfdiService _createCfdiService;

    public CfdiController(ICreateCfdiUseCase createCfdiUseCase,  
                          ICfdiFiscalRulesValidator cfdiFiscalRulesValidator,
                          IMessagesProvider messagesProvider,
                          ICfdiTotalsValidator cfdiTotalsValidator,
                          ICreateCfdiService createCfdiService)
    {
        _messagesProvider = messagesProvider;
        _createCfdiUseCase = createCfdiUseCase;
        _fiscalRulesValidator = cfdiFiscalRulesValidator;
        _cfdiTotalsValidator = cfdiTotalsValidator;
        _createCfdiService = createCfdiService;
    }


    /// <summary>
    /// Timbrado de CFDI 4.0 a partir de JSON (FiscalFlow genera XML, sello y timbre)
    /// </summary>
    [HttpPost("timbrar")]
    [ProducesResponseType(typeof(CreateCfdiRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Timbrar([FromBody] CreateCfdiRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createCfdiUseCase.ExecuteAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        // validaciones fiscales con catálogos SAT
        var fiscalErrors = await _fiscalRulesValidator.ValidateAsync(request, cancellationToken);

        if (fiscalErrors.Any())
        {
            return BadRequest(new CfdiErrorResponseDto { 
                IsSuccess=false, 
                Message = _messagesProvider.GetError("FiscalValidationFailed"), 
                Errors = fiscalErrors
            });
        }

        // validar los totales de un CFDI
        var cfdiTotalErrors = _cfdiTotalsValidator.Validate(request);

        if (cfdiTotalErrors.Any())
        {
            return BadRequest(new CfdiErrorResponseDto
            {
                IsSuccess = false,
                Message = _messagesProvider.GetError("TotalsValidationFailed"),
                Errors = cfdiTotalErrors
            });
        }

        // crear el XML:CFDI
        var createCfdiResult = await _createCfdiService.Execute(request);

        if (!createCfdiResult.IsSuccess)
        {
            return BadRequest(createCfdiResult);
        }

        // Validar el XML CFDI 4.0 generado contra el XSD oficial del SAT

        return Ok(result);
    }




}
