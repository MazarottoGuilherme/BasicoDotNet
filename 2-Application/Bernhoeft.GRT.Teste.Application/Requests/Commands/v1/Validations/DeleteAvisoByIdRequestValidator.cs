using System.Data;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Common.Validation;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class DeleteAvisoByIdRequestValidator: AbstractValidator<DeleteAvisoByIdRequest>
{
    private readonly IAvisoRepository _repo;

    public DeleteAvisoByIdRequestValidator(IAvisoRepository avisoRepository)
    {
        _repo = avisoRepository;
        RuleFor(x => x.Id)
            .MustBeValidAvisoId(_repo);
    }

}