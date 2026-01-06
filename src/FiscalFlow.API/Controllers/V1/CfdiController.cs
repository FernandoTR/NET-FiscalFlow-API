using Asp.Versioning;
using FiscalFlow.Application.DTOs.Cfdi;
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

    public CfdiController(ICreateCfdiUseCase createCfdiUseCase)
    {
        _createCfdiUseCase = createCfdiUseCase;
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

        return Ok(result);
    }




}
