using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Enums;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Common.Validation;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, int> MustBeValidAvisoId<T>(
        this IRuleBuilder<T, int> rule,
        IAvisoRepository repo)
    {
        return rule
            .GreaterThan(0).WithMessage("Id deve ser maior que zero")
            .MustAsync(async (id, ct) =>
            {
                var aviso = await repo.ObterAvisoByIdAsync(id, TrackingBehavior.NoTracking);
                return aviso != null;
            })
            .WithMessage("Aviso não encontrado.");
    }

}