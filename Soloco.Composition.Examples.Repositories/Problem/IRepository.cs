using System.Collections.Generic;

namespace TC.Remixing.Repositories.Problem
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        TEntity RetrieveById(TKey id);

        IEnumerable<TEntity> RetrieveAll();
        long Count();

        void Persist(TEntity entity);

        void Delete(TKey key);
        void Delete(TEntity entity);
    }
}