
namespace Soloco.Composition.Example.Repositories.Services
{
    [Implementation(typeof(EntityDeleter<,>))]
    public interface IEntityDeleter<TEntity, TKey>
        where TEntity : class, new()
    {
        void Delete(TKey key);
        void Delete(TEntity entity);
    }
}