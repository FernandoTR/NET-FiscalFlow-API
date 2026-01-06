using FiscalFlow.Application.DTOs.Cfdi;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFlow.Application.Validators.Cfdi.Validation;

public sealed class TrasladoValidator: AbstractValidator<TrasladoConceptoDto>
{
    public TrasladoValidator()
    {
        RuleFor(x => x.Base)
           .GreaterThan(0)
           .WithMessage("El atributo Base debe ser mayor que cero.");

        RuleFor(x => x.TipoFactor)
            .Must(x => x is "Tasa" or "Cuota" or "Exento")
            .WithMessage("El atributo TipoFactor debe contener uno de los valores permitidos: Tasa, Cuota o Exento.");

        RuleFor(x => x.TasaOCuota)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El atributo TasaOCuota debe ser mayor o igual a cero.");

        RuleFor(x => x.Importe)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El atributo Importe debe ser mayor o igual a cero.");
    }
}
