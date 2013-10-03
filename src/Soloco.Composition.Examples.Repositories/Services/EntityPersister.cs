
using Soloco.Composition.Construction;

namespace Soloco.Composition.Example.Repositories.Services
{
    class EntityPersister<TEntity> : IEntityPersister<TEntity>
        where TEntity : class, new()
    {
        [This]
        private readonly Repository _repository;

        public void Persist(TEntity entity)
        {
            _repository.Session.Save(entity);
        }
    }
}