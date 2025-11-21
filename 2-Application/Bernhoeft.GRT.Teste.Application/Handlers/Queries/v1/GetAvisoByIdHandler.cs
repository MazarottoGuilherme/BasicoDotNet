using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;

public class GetAvisoByIdHandler  : IRequestHandler<GetAvisoByIdRequest, IOperationResult<GetAvisoByIdResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private IContext _context
        => _serviceProvider.GetRequiredService<IContext>();
    private IValidator<GetAvisoByIdRequest> _validator
        => _serviceProvider.GetRequiredService<IValidator<GetAvisoByIdRequest>>();
    private IAvisoRepository _avisoRepository
        => _serviceProvider.GetRequiredService<IAvisoRepository>();


    public GetAvisoByIdHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task<IOperationResult<GetAvisoByIdResponse>> Handle(GetAvisoByIdRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return OperationResult<GetAvisoByIdResponse>
                .Return(CustomHttpStatusCode.BadRequest, new GetAvisoByIdResponse
                {
                    Id = request.Id
                })
                .AddMessage(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var entity = await _avisoRepository.ObterAvisoByIdAsync(request.Id, TrackingBehavior.NoTracking);

        if (entity == null)
            return OperationResult<GetAvisoByIdResponse>.ReturnNoContent();

        return OperationResult<GetAvisoByIdResponse>.ReturnOk((GetAvisoByIdResponse)entity);
    }

}