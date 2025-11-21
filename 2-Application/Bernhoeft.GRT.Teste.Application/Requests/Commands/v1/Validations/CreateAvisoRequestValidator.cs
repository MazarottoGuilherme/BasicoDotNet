using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class CreateAvisoRequestValidator : AbstractValidator<CreateAvisoRequest>
{
    public CreateAvisoRequestValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Título é obrigatório")
            .MaximumLength(100);
        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("Mensagem é obrigatória");
    }
}