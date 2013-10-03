using System.Collections.Generic;
using Remotion.Implementation;

namespace TC.Remixing.Repositories.Core
{
    [ConcreteImplementation(typeof(AllEntiyRetriever<>))]
    public interface IAllEntiyRetriever<TEntity>
    {
        IEnumerable<TEntity> RetrieveAll();
    }
}