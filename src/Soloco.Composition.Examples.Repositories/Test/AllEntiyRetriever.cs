using System;
using System.Collections.Generic;

namespace TC.Remixing.Repositories.Core
{
    class AllEntiyRetriever<TEntity> : IAllEntiyRetriever<TEntity>
    {
        public IEnumerable<TEntity> RetrieveAll()
        {
            throw new NotImplementedException();
        }
    }
}