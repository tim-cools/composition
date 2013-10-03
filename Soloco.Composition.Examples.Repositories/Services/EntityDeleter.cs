
using Soloco.Composition.Construction;

namespace Soloco.Composition.Example.Repositories.Services
{
    class EntityDeleter<TEntity, TKey> : IEntityDeleter<TEntity, TKey>
        where TEntity : class, new()
    {
        [This]
        private readonly Repository _repository;

        public void Delete(TEntity entity)
        {
            _repository.Session
                       .Delete(entity);
        }

        public void Delete(TKey key)
        {
            var entity = _repository.Session
                                    .Get(typeof (TEntity), key);
            _repository.Session.Delete(entity);
        }
    }
}