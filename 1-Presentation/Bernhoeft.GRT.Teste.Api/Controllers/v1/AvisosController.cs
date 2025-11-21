using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v1
{
    /// <response code="401">Não Autenticado.</response>
    /// <response code="403">Não Autorizado.</response>
    /// <response code="500">Erro Interno no Servidor.</response>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {

        /// <summary>
        /// Retorna Todos os Avisos Cadastrados para Tela de Edição.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Lista com Todos os Avisos.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Sem Avisos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<GetAvisosResponse>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<object> GetAvisos(CancellationToken cancellationToken)
            => await Mediator.Send(new GetAvisosRequest(), cancellationToken);

        /// <summary>
        /// Retorna um aviso específico com base em um ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Um único aviso</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Aviso não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<GetAvisoByIdResponse>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDocumentationRestResult<object>))]
        [HttpGet("{id:int}")]
        public async Task<object> GetAvisosById(int id, CancellationToken cancellationToken)
            => await Mediator.Send(new GetAvisoByIdRequest { Id = id }, cancellationToken);


        /// <summary>
        /// Deleta um aviso com base no Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Um valor bool indicando como ocorreu o delete</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Aviso não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<DeleteAvisoByIdResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDocumentationRestResult<object>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}")]
        public async Task<object> DeleteAvisoById(int id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAvisoByIdRequest {Id = id}, cancellationToken);

        /// <summary>
        /// Cria um novo aviso
        /// </summary>
        /// <param name="Aviso DTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Um valor bool indicando como ocorreu o delete</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Aviso não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<CreateAvisoResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDocumentationRestResult<object>))]
        [HttpPost]
        public async Task<object> CriarAviso(CreateAvisoRequest avisoRequest, CancellationToken cancellationToken)
            => await Mediator.Send(avisoRequest, cancellationToken);

        /// <summary>
        /// Edita a mensagem de um aviso
        /// </summary>
        /// <param name="Id do aviso"></param>
        /// <param name="mensagem do aviso"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna o objeto atualizado</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Aviso não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<EditarAvisoResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IDocumentationRestResult<object>))]
        [HttpPut("{id:int}")]
        public async Task<object> EditarAviso(int id, [FromBody] string mensagem, CancellationToken cancellationToken)
            => await Mediator.Send(new EditarAvisoRequest {Id = id, Mensagem = mensagem}, cancellationToken);

    }
}