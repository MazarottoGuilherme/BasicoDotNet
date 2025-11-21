using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class EditarAvisoRequestValidator : AbstractValidator<EditarAvisoRequest>
{
    public EditarAvisoRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero");

        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("A mensagem não pode ser vazio");
    }


}