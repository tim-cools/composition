
namespace Soloco.Composition.Example.Repositories.Services
{
    [Implementation(typeof(EntityPersister<>))]
    public interface IEntityPersister<TEntity>
        where TEntity : class, new()
    {
        void Persist(TEntity entity);
    }
}