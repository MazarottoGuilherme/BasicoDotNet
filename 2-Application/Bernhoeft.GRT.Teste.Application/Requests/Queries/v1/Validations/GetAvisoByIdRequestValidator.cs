using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;

public class GetAvisoByIdRequestValidator : AbstractValidator<GetAvisoByIdRequest>
{

    public GetAvisoByIdRequestValidator()
    {

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero");
    }
}