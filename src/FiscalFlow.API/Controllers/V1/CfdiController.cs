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
    private readonly ICreateCfdiUseCase _createCfdiUseCase;
    private readonly ISatCatalogService _satCatalogService;
    private readonly ICfdiFiscalRulesValidator _fiscalRulesValidator;
    private readonly IMessagesProvider _messagesProvider;

    public CfdiController(ICreateCfdiUseCase createCfdiUseCase, 
                          ISatCatalogService satCatalogService, 
                          ICfdiFiscalRulesValidator cfdiFiscalRulesValidator,
                          IMessagesProvider messagesProvider)
    {
        _createCfdiUseCase = createCfdiUseCase;
        _satCatalogService = satCatalogService;
        _fiscalRulesValidator = cfdiFiscalRulesValidator;
        _messagesProvider = messagesProvider;
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



        return Ok(result);
    }




}
