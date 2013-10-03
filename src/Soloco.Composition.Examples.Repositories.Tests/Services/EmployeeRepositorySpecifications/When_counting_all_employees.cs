using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.EmployeeRepositorySpecifications
{
    public class When_counting_all_employees
    {
        [TestClass]
        public class given_there_are_employees_in_the_database : EmployeeRepositorySpecification
        {
            private int _countActual;
            private int _countExpected;

            protected override void Arrange()
            {
                base.Arrange();

                var employees = TestEmployeeFactory.CreateList();
                _countExpected = employees.Count();
                Session.PersistAll(employees);
            }

            protected override void Act()
            {
                _countActual = Repository.Count();
            }

            [TestMethod]
            public void then_all_the_employees_should_be_returned()
            {
                Assert.AreEqual(_countExpected, _countActual);
            }
        }

        [TestClass]
        public class given_no_employees_are_in_the_database : EmployeeRepositorySpecification
        {
            private long _countActual;
            
            protected override void Act()
            {
                _countActual = Repository.Count();
            }

            [TestMethod]
            public void then_all_the_employees_should_be_returned()
            {
                Assert.AreEqual(0, _countActual);
            }
        }
    }
}