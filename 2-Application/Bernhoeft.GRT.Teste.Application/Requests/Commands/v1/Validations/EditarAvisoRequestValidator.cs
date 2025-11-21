using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Application.Common.Validation;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class EditarAvisoRequestValidator : AbstractValidator<EditarAvisoRequest>
{
    private readonly IAvisoRepository _repo;

    public EditarAvisoRequestValidator(IAvisoRepository avisoRepository)
    {
        _repo = avisoRepository;

        RuleFor(x => x.Id)
            .MustBeValidAvisoId(_repo);

        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("A mensagem não pode ser vazio");
    }


}