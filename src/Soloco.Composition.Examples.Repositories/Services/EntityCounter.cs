
using NHibernate.Criterion;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Example.Repositories.Services
{
    class EntityCounter<TEntity> : IEntityCounter<TEntity>
        where TEntity : class, new()
    {
        [This]
        private readonly Repository _repository;

        public int Count()
        {
            return _repository.Session
                .CreateCriteria(typeof(TEntity))
                .SetProjection(Projections.Count(Projections.Id()))
                .UniqueResult<int>();
        }
    }
}