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
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class EditarAvisoHandler : IRequestHandler<EditarAvisoRequest, IOperationResult<EditarAvisoResponse>>
{
    private readonly IServiceProvider _serviceProvider;

    private IContext _context
        => _serviceProvider.GetRequiredService<IContext>();

    private IAvisoRepository _avisoRepository
        => _serviceProvider.GetRequiredService<IAvisoRepository>();

    private IValidator<EditarAvisoRequest> _validator
        => _serviceProvider.GetRequiredService<IValidator<EditarAvisoRequest>>();

    public EditarAvisoHandler(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public async Task<IOperationResult<EditarAvisoResponse>> Handle(EditarAvisoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return OperationResult<EditarAvisoResponse>
                .Return(CustomHttpStatusCode.BadRequest, new EditarAvisoResponse()
                {
                    Id = request.Id,
                    Mensagem = request.Mensagem
                })
                .AddMessage(validationResult.Errors.Select(e => e.ErrorMessage));
        }


        var entity = await _avisoRepository.ObterAvisoByIdAsync(request.Id, TrackingBehavior.NoTracking);

        if (entity == null)
            return OperationResult<EditarAvisoResponse>.ReturnNoContent();

        entity.Mensagem = request.Mensagem;
        entity.DataAlteracao = DateTime.Now;

        await _avisoRepository.EditarAvisoAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return OperationResult<EditarAvisoResponse>.ReturnOk(new EditarAvisoResponse
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Mensagem = entity.Mensagem
        });

    }

}