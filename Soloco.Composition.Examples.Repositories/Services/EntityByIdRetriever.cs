
using Soloco.Composition.Construction;

namespace Soloco.Composition.Example.Repositories.Services
{
    class EntityByIdRetriever<TEntity, TKey> : IEntityByIdRetriever<TEntity, TKey>
        where TEntity : class, new()
    {
        [This]
        private readonly Repository _repository;
        
        public TEntity RetrieveById(TKey id)
        {
            return _repository.Session.Get<TEntity>(id);
        }
    }
}