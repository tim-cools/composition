using Remotion.Implementation;

namespace TC.Remixing.Repositories.Core
{
    [ConcreteImplementation(typeof(EntityPersister<>))]
    public interface IEntityPersister<TEntity>
    {
        void Persist(TEntity entity);
    }
}