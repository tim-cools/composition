using System;

namespace TC.Remixing.Repositories.Core
{
    class EntityByIdRetriever<TEntity, TKey> : IEntityByIdRetriever<TEntity, TKey>
    {
        public TEntity RetrieveById(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}