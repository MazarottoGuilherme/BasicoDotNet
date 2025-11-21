using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.ContractWeb.Infra.Persistence.SqlServer.ContractStore.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        public AvisoRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            return query.Where(x => x.Ativo == true).ToListAsync();
        }

        public Task<AvisoEntity?> ObterAvisoByIdAsync(int id, TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            return query.Where(x => x.Id == id && x.Ativo == true).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> DesativarAvisoAsync(int id, CancellationToken cancellationToken = default)
        {
            var query = Set;

            var entity = await query.FirstOrDefaultAsync(x => x.Id == id && x.Ativo == true, cancellationToken);

            if (entity == null)
                return false;

            entity.Ativo = false;
            return true;
        }

        public async Task<AvisoEntity> CreateAvisoAsync(CreateAvisoDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = new AvisoEntity
            {
                Titulo = dto.Titulo,
                Mensagem = dto.Mensagem,
                Ativo = true
            };

            await Set.AddAsync(entity, cancellationToken);

            return entity;
        }

        public Task EditarAvisoAsync(AvisoEntity entity, CancellationToken cancellationToken = default)
        {
            Set.Update(entity);
            return Task.CompletedTask;
        }

    }
}