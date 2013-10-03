using System.Collections.Generic;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public static class TestEmployeeFactory
    {
        public static IEnumerable<Employee> CreateList()
        {
            return new List<Employee>
                       {
                           new Employee { FirstName = "First1", CountryCode = "BE", LastName = "Last1" }, 
                           new Employee { FirstName = "First2", CountryCode = "BE", LastName = "Last2" }, 
                           new Employee { FirstName = "First3", CountryCode = "BE", LastName = "Last3" }, 
                           new Employee { FirstName = "First4", CountryCode = "NL", LastName = "Last4" }, 
                           new Employee { FirstName = "First5", CountryCode = "NL", LastName = "Last5" }, 
                           new Employee { FirstName = "First6", CountryCode = "NL", LastName = "Last6" }, 
                       };
        }

        public static Employee Create()
        {
            return new Employee { FirstName = "First1", CountryCode = "BE", LastName = "Last1" };
        }
    }
}