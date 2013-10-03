

namespace Soloco.Composition.Example.Repositories.Services
{
    [Implementation(typeof(EntityByIdRetriever<,>))]
    public interface IEntityByIdRetriever<TEntity, TKey>
        where TEntity : class, new()
    {
        TEntity RetrieveById(TKey id);
    }
}