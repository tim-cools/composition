using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.EmployeeRepositorySpecifications
{
    [TestClass]
    public class When_deleting_employees_by_id
    {
        [TestClass]
        public class given_no_employees_are_in_the_database : EmployeeRepositorySpecification
        {
            private IEnumerable<Employee> _employees;

            protected override void Arrange()
            {
                base.Arrange();

                _employees = TestEmployeeFactory.CreateList();
                Session.PersistAll(_employees); 
            }

            protected override void Act()
            {
                foreach (var employee in _employees)
                {
                    Repository.Delete(employee.Id);
                }
            }

            [TestMethod]
            public void then_no_the_employees_should_be_returned()
            {
                var employees = Session.GetAll<Employee>();
                Assert.AreEqual(0, employees.Count);
            }
        }
    }
}