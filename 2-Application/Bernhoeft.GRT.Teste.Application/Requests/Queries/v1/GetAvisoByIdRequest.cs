using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class GetAvisoByIdRequest : IRequest<IOperationResult<GetAvisoByIdResponse>>
    {
        public int Id { get; set; }
    }
}