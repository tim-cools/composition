using Remotion.Implementation;

namespace TC.Remixing.Repositories.Core
{
    [ConcreteImplementation(typeof(EntityDeleter<>))]
    public interface IEntityDeleter<TEntity>
    {
        void Delete(TEntity entity);
    }
}