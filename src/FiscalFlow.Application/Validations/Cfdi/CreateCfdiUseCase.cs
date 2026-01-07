using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Interfaces.Message;
using FiscalFlow.Application.Interfaces.Validations;
using FluentValidation;

namespace FiscalFlow.Application.Validators.Cfdi;

public sealed class CreateCfdiUseCase : ICreateCfdiUseCase
{
    private readonly IValidator<CreateCfdiRequestDto> _validator;
    private readonly IMessagesProvider _messagesProvider;

    public CreateCfdiUseCase(IValidator<CreateCfdiRequestDto> validator, IMessagesProvider messagesProvider)
    {
        _validator = validator;
        _messagesProvider = messagesProvider;
    }

    public async Task<CfdiResponseDto> ExecuteAsync(CreateCfdiRequestDto request, CancellationToken ct)
    {
        // Validamos la estructura del JSON recibido
        var validationResult = await _validator.ValidateAsync(request, ct);

        // Si la validación falla, retornamos los errores encontrados
        if (!validationResult.IsValid)
        {
            return new CfdiErrorResponseDto()
            {
                IsSuccess = false,
                Message = _messagesProvider.GetError("InvalidCfdiStructure"),
                Errors = validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                })
            };
            //throw new ValidationException(validationResult.Errors);
        }
               
        // ✔ JSON válido
        // ✔ Reglas fiscales mínimas cumplidas 
        return new CfdiErrorResponseDto()
        {
            IsSuccess = true,
        };
    }
}

