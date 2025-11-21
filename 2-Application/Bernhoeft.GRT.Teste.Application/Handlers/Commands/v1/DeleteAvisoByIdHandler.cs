using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class DeleteAvisoByIdHandler: IRequestHandler<DeleteAvisoByIdRequest, IOperationResult<DeleteAvisoByIdResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private IContext _context => _serviceProvider.GetRequiredService<IContext>();

    private IValidator<DeleteAvisoByIdRequest> _validator
        => _serviceProvider.GetRequiredService<IValidator<DeleteAvisoByIdRequest>>();
    private IAvisoRepository _avisoRepository
        => _serviceProvider.GetRequiredService<IAvisoRepository>();

    public DeleteAvisoByIdHandler(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task<IOperationResult<DeleteAvisoByIdResponse>> Handle(DeleteAvisoByIdRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return OperationResult<DeleteAvisoByIdResponse>
                .Return(CustomHttpStatusCode.BadRequest, new DeleteAvisoByIdResponse()
                {
                    Sucesso = false
                })
                .AddMessage(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var entity = await _avisoRepository.DesativarAvisoAsync(request.Id, cancellationToken);

        if(entity == false)
            return OperationResult<DeleteAvisoByIdResponse>.ReturnNoContent();

        _context.SaveChanges();

        return OperationResult<DeleteAvisoByIdResponse>.ReturnOk(
            new DeleteAvisoByIdResponse { Sucesso = entity }
        );
    }
}