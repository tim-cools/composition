
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace TC.Remixing.Repositories.Problem
{
    class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class, new()
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public TEntity RetrieveById(TKey id)
        {
            return _session.Load<TEntity>(id);
        }

        public IEnumerable<TEntity> RetrieveAll()
        {
            var criteria = _session.CreateCriteria(typeof(TEntity));
            return criteria.List<TEntity>();
        }

        public long Count()
        {
            return _session.CreateCriteria(typeof(TEntity))
                           .SetProjection(Projections.Count(Projections.Id()))
                           .UniqueResult<int>();
        }

        public void Persist(TEntity entity)
        {
            _session.Persist(entity);
        }

        public void Delete(TKey key)
        {
            var entity = _session.Get(typeof(TEntity), key);
            _session.Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            _session.Delete(entity);
        }
    }
}