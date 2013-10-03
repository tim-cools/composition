using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.EmployeeRepositorySpecifications
{
    [TestClass]
    public class When_persisting_employees_given_there_are_employees_in_the_database : EmployeeRepositorySpecification
    {
        private Employee _employee;

        protected override void Arrange()
        {
            base.Arrange();

            _employee = TestEmployeeFactory.Create();
        }

        protected override void Act()
        {
            Repository.Persist(_employee);
        }

        [TestMethod]
        public void then_the_employees_should_exist_in_the_database()
        {
            var actual = Session.Get<Employee>(_employee.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(_employee, actual);
        }
    }
}