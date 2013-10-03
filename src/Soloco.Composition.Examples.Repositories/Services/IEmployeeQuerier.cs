
using System.Collections.Generic;

namespace Soloco.Composition.Example.Repositories.Services
{
    [Implementation(typeof(EmployeeQuerier))]
    public interface IEmployeeQuerier
    {
        IList<Employee> RetrieveByCountry(string countryCode);
    }
}