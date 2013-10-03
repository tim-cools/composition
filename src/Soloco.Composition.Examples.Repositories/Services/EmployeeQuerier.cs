
using System.Collections.Generic;
using NHibernate.Criterion;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Example.Repositories.Services
{
    public class EmployeeQuerier : IEmployeeQuerier
    {
        [This]
        private readonly Repository _repository;

        public IList<Employee> RetrieveByCountry(string countryCode)
        {
            var criteria = _repository.Session.CreateCriteria(typeof(Employee));
            criteria.Add(Restrictions.Eq("CountryCode", countryCode));
            return criteria.List<Employee>();
        }
    }
}