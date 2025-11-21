using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Common.Validation;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;

public class GetAvisoByIdRequestValidator : AbstractValidator<GetAvisoByIdRequest>
{
    private readonly IAvisoRepository _repo;

    public GetAvisoByIdRequestValidator(IAvisoRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.Id)
            .MustBeValidAvisoId(_repo);
    }
}