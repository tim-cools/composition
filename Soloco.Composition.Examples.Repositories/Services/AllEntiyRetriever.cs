
using System.Collections.Generic;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Example.Repositories.Services
{
    internal class AllEntiyRetriever<TEntity> : IAllEntiyRetriever<TEntity>
        where TEntity : class, new()
    {
        [This]
        private readonly Repository _repository;

        public IEnumerable<TEntity> RetrieveAll()
        {
            var criteria = _repository.Session.CreateCriteria(typeof(TEntity));
            return criteria.List<TEntity>();
        }
    }
}