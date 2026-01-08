using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Cfdi;
using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Application.Interfaces.Message;

namespace FiscalFlow.Application.Validations.CfdiFiscalRules;

public sealed class CfdiFiscalRulesValidator : ICfdiFiscalRulesValidator
{
    private readonly ISatCatalogService _catalogService;
    private readonly IMessagesProvider _messagesProvider;

    public CfdiFiscalRulesValidator(ISatCatalogService catalogs, IMessagesProvider messagesProvider)
    {
        _catalogService = catalogs;
        _messagesProvider = messagesProvider;
    }

    public async Task<IReadOnlyList<CfdiErrorDetailDto>> ValidateAsync(CreateCfdiRequestDto request, CancellationToken ct)
    {
        var errors = new List<CfdiErrorDetailDto>();

        await ValidateComprobanteAsync(request, errors, ct);
        await ValidateEmisorAsync(request, errors, ct);
        await ValidateReceptorAsync(request, errors, ct);
        await ValidateConceptosAsync(request, errors, ct);
        await ValidateImpuestosAsync(request, errors, ct);

        return errors; 
    }

    private async Task ValidateComprobanteAsync(CreateCfdiRequestDto dto, List<CfdiErrorDetailDto> errors, CancellationToken ct)
    {

        if (!await _catalogService.ExistsAsync("c_FormaPago", dto.Comprobante.FormaPago, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.FormaPago",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Comprobante.FormaPago, "c_FormaPago")
            });              


        if (!await _catalogService.ExistsAsync("c_MetodoPago", dto.Comprobante.MetodoPago, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.MetodoPago",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Comprobante.MetodoPago, "c_MetodoPago")
            });
        

        if (!await _catalogService.IsCombinationAllowedAsync(
                "c_FormaPago", dto.Comprobante.FormaPago,
                "c_MetodoPago", dto.Comprobante.MetodoPago, ct))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.MetodoPago",
                Message = string.Format(_messagesProvider.GetError("InvalidCatalogCombination"), "FormaPago", dto.Comprobante.FormaPago, "MetodoPago", dto.Comprobante.MetodoPago)
            });
        }

        if (!await _catalogService.ExistsAsync("c_Moneda", dto.Comprobante.Moneda, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.Moneda",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Comprobante.Moneda, "c_Moneda")
            });

        if (!await _catalogService.ExistsAsync("c_TipoDeComprobante", dto.Comprobante.TipoDeComprobante, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Comprobante.TipoDeComprobante",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Comprobante.TipoDeComprobante, "c_TipoDeComprobante")
            });
        

    }

    private async Task ValidateEmisorAsync(CreateCfdiRequestDto dto, List<CfdiErrorDetailDto> errors, CancellationToken ct)
    {
        if (!await _catalogService.ExistsAsync("c_RegimenFiscal", dto.Emisor.RegimenFiscal, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Emisor.RegimenFiscal",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Emisor.RegimenFiscal, "c_RegimenFiscal")
            });
    }

    private async Task ValidateReceptorAsync(CreateCfdiRequestDto dto, List<CfdiErrorDetailDto> errors, CancellationToken ct)
    {
        if (!await _catalogService.ExistsAsync("c_UsoCFDI", dto.Receptor.UsoCFDI, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Receptor.UsoCFDI",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Receptor.UsoCFDI, "c_UsoCFDI")
            });

        if (!await _catalogService.ExistsAsync("c_RegimenFiscal",dto.Receptor.RegimenFiscalReceptor, ct))
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Receptor.RegimenFiscalReceptor",
                Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), dto.Receptor.RegimenFiscalReceptor, "c_RegimenFiscal")
            });
       

        if (!await _catalogService.IsCombinationAllowedAsync(
                "c_RegimenFiscal",
                dto.Receptor.RegimenFiscalReceptor,
                "c_UsoCFDI",
                dto.Receptor.UsoCFDI,
                ct))
        {
            errors.Add(new CfdiErrorDetailDto
            {
                Field = "Receptor.UsoCFDI",
                Message = string.Format(_messagesProvider.GetError("InvalidCatalogCombination"), "UsoCFDI", dto.Receptor.UsoCFDI, "RegimenFiscalReceptor", dto.Receptor.RegimenFiscalReceptor)
            });
        }
    }

    private async Task ValidateConceptosAsync(CreateCfdiRequestDto dto, List<CfdiErrorDetailDto> errors, CancellationToken ct)
    {
        foreach (var (concepto, index) in dto.Conceptos.Select((c, i) => (c, i)))
        {
            if (!await _catalogService.ExistsAsync("c_ClaveProdServ", concepto.ClaveProdServ, ct))
                errors.Add(new CfdiErrorDetailDto
                {
                    Field = $"Conceptos[{index}].ClaveProdServ",
                    Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), concepto.ClaveProdServ, "c_ClaveProdServ")
                });         

            if (!await _catalogService.ExistsAsync("c_ClaveUnidad", concepto.ClaveUnidad, ct))
                errors.Add(new CfdiErrorDetailDto
                {
                    Field = $"Conceptos[{index}].ClaveUnidad",
                    Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), concepto.ClaveUnidad, "c_ClaveUnidad")
                });

            if (!await _catalogService.ExistsAsync("c_ObjetoImp", concepto.ObjetoImp, ct))
                errors.Add(new CfdiErrorDetailDto
                {
                    Field = $"Conceptos[{index}].ObjetoImp",
                    Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), concepto.ObjetoImp, "c_ObjetoImp")
                });

        }
    }

    private async Task ValidateImpuestosAsync(CreateCfdiRequestDto dto, List<CfdiErrorDetailDto> errors, CancellationToken ct)
    {
        foreach (var traslado in dto.Impuestos?.Traslados ?? Enumerable.Empty<TrasladoGlobalDto>())
        {
            if (!await _catalogService.ExistsAsync("c_Impuesto", traslado.Impuesto, ct))
                errors.Add(new CfdiErrorDetailDto
                {
                    Field = $"Impuestos.Traslados.Impuesto",
                    Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), traslado.Impuesto, "c_Impuesto")
                });

            if (!await _catalogService.ExistsAsync("c_TipoFactor", traslado.TipoFactor, ct))
                errors.Add(new CfdiErrorDetailDto
                {
                    Field = $"Impuestos.Traslados.TipoFactor",
                    Message = string.Format(_messagesProvider.GetError("CatalogKeyNotFound"), traslado.TipoFactor, "c_TipoFactor")
                });
        }
    }


}
