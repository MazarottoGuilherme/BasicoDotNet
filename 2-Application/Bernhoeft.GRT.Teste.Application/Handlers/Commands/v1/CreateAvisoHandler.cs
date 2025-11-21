using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class CreateAvisoHandler : IRequestHandler<CreateAvisoRequest, IOperationResult<CreateAvisoResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private IContext _context => _serviceProvider.GetRequiredService<IContext>();

    private IValidator<CreateAvisoRequest> _validator
        => _serviceProvider.GetRequiredService<IValidator<CreateAvisoRequest>>();

    private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

    public CreateAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task<IOperationResult<CreateAvisoResponse>> Handle(CreateAvisoRequest request,
        CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return OperationResult<CreateAvisoResponse>
                .Return(CustomHttpStatusCode.BadRequest, new CreateAvisoResponse()
                {
                    Titulo = request.Titulo,
                    Sucesso = false
                })
                .AddMessage(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var dto = new CreateAvisoDTO { Titulo = request.Titulo, Mensagem = request.Mensagem };

        var entity = await _avisoRepository.CreateAvisoAsync(dto, cancellationToken);

        _context.SaveChanges();

        var response = new CreateAvisoResponse
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Sucesso = true
        };

        return OperationResult<CreateAvisoResponse>.Return(CustomHttpStatusCode.Created, response);
    }

}