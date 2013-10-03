using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.StoreSpecifications
{
    [TestClass]
    public class When_adding_an_employee : UnitSpecification
    {
        private Store _store;
        private Employee _employee;

        protected override void Arrange()
        {
            base.Arrange();

            _store = new Store();
            _employee = new Employee();
        } 

        protected override void Act()
        {
            _store.AddEmployee(_employee);
        }

        [TestMethod]
        public void then_the_store_should_contain_the_product()
        {
            Assert.IsTrue(_store.Staff.Contains(_employee));
        }

        [TestMethod]
        public void then_the_employee_should_be_assigned_to_the_store()
        {
            Assert.AreEqual(_store, _employee.Store);
        }

        [TestMethod]
        public void then_the_store_should_have_one_employee_in_his_staff_collection()
        {
            int count = 0;
            foreach (var employee in _store.Staff)
            {
                Assert.AreSame(_employee, employee);
                count++;
            }
            Assert.AreEqual(1, count);
        }
    }
}