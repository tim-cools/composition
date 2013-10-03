using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.EmployeeRepositorySpecifications
{
    public class When_retrieving_all_employees
    {
        [TestClass]
        public class given_some_employees_are_in_the_database : EmployeeRepositorySpecification
        {
            private IEnumerable<Employee> _employeesActual;
            private IEnumerable<Employee> _employeesExpected;

            protected override void Arrange()
            {
                base.Arrange();

                _employeesExpected = TestEmployeeFactory.CreateList();
                Session.PersistAll(_employeesExpected);
            }

            protected override void Act()
            {
                _employeesActual = Repository.RetrieveAll();
            }

            [TestMethod]
            public void then_all_the_employees_should_be_returned()
            {
                Assert.IsNotNull(_employeesActual);
                Assert.AreEqual(_employeesExpected.Count(), _employeesActual.Count());

                foreach (var actual in _employeesActual)
                {
                    var expected = _employeesExpected.SingleOrDefault(c => c.Id == actual.Id);
                    Assert.AreEqual(actual, expected);
                }
            }
        }

        [TestClass]
        public class given_no_employees_are_in_the_database : EmployeeRepositorySpecification
        {
            private IEnumerable<Employee> _employeesActual;

            protected override void Act()
            {
                _employeesActual = Repository.RetrieveAll();
            }

            [TestMethod]
            public void then_no_the_employees_should_be_returned()
            {
                Assert.IsNotNull(_employeesActual);
                Assert.AreEqual(0, _employeesActual.Count());
            }
        }
    }
}