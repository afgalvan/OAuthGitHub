using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OAuthGitHub.Api.Domain
{
    public interface IRepository<TEntity, in TId>
    {
        public Task<TEntity> Save(TEntity entity, CancellationToken cancellation);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetById(TId id);
        public Task Remove(TEntity entity);
    }
}
