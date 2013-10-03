
using System.Collections.Generic;

namespace Soloco.Composition.Example.Repositories.Services
{
    [Implementation(typeof(AllEntiyRetriever<>))]
    public interface IAllEntiyRetriever<TEntity>
        where TEntity : class, new ()
    {
        IEnumerable<TEntity> RetrieveAll();
    }
}