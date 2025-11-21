using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;

namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default);

        Task<AvisoEntity?> ObterAvisoByIdAsync(int id, TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default);

        Task<bool> DesativarAvisoAsync(int id, CancellationToken cancellationToken = default);

        Task<AvisoEntity> CreateAvisoAsync(CreateAvisoDTO dto, CancellationToken cancellationToken = default);

        Task EditarAvisoAsync(AvisoEntity entity, CancellationToken cancellationToken = default);
    }
}