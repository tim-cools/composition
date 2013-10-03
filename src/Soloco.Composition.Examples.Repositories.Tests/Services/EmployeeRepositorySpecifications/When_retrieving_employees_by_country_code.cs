using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.EmployeeRepositorySpecifications
{
    public class When_retrieving_employees_by_country_code
    {
        [TestClass]
        public class given_there_are_employees_with_the_country_code_in_the_database : EmployeeRepositorySpecification
        {
            private const string CountryCode = "BE";
            private IList<Employee> _employeesActual;
            private IList<Employee> _employeesExpected;

            protected override void Arrange()
            {
                base.Arrange();

                var employees = TestEmployeeFactory.CreateList();
                _employeesExpected = employees.Where(e => e.CountryCode == CountryCode).ToList();
                Session.PersistAll(employees);
            }

            protected override void Act()
            {
                _employeesActual = Repository.RetrieveByCountry(CountryCode);
            }

            [TestMethod]
            public void then_all_the_employees_should_be_returned()
            {
                Assert.IsNotNull(_employeesActual);
                Assert.AreEqual(_employeesExpected.Count, _employeesActual.Count);
                foreach (var expected in _employeesExpected)
                {
                    var employee = _employeesActual.SingleOrDefault(e => e.Id == expected.Id);
                    Assert.IsNotNull(employee);
                    _employeesActual.Remove(employee);
                }
                Assert.AreEqual(0, _employeesActual.Count);
            }
        }
        [TestClass]
        public class given_there_are_no_employees_with_the_country_code_in_the_database : EmployeeRepositorySpecification
        {
            private const string CountryCode = "DE";
            private IList<Employee> _employeesActual;

            protected override void Arrange()
            {
                base.Arrange();

                var employees = TestEmployeeFactory.CreateList();
                Session.PersistAll(employees);
            }

            protected override void Act()
            {
                _employeesActual = Repository.RetrieveByCountry(CountryCode);
            }

            [TestMethod]
            public void then_all_the_employees_should_be_returned()
            {
                Assert.IsNotNull(_employeesActual);
                Assert.AreEqual(0, _employeesActual.Count);
            }
        }
    }
}