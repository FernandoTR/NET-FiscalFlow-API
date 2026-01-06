using FiscalFlow.Application.DTOs.Cfdi;
using FiscalFlow.Application.Validations.Cfdi.Validators;
using FluentValidation;

namespace FiscalFlow.Application.Validators.Cfdi.Validation;

public sealed class ConceptoImpuestosValidator
    : AbstractValidator<ConceptoImpuestosDto>
{
    public ConceptoImpuestosValidator()
    {
        RuleForEach(x => x.Traslados)
            .SetValidator(new TrasladoValidator());

        RuleForEach(x => x.Retenciones)
            .SetValidator(new RetencionValidator());
    }
}

