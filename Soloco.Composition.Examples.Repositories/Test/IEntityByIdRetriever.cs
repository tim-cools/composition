using Remotion.Implementation;

namespace TC.Remixing.Repositories.Core
{
    [ConcreteImplementation(typeof(EntityByIdRetriever<,>))]
    public interface IEntityByIdRetriever<TEntity, TKey>
    {
        TEntity RetrieveById(TKey id);
    }
}