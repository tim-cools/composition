using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.StoreSpecifications
{
    [TestClass]
    public class When_adding_a_product : UnitSpecification
    {
        private Store _store;
        private Product _product;

        protected override void Arrange()
        {
            base.Arrange();

            _store = new Store();
            _product = new Product();
        } 

        protected override void Act()
        {
            _store.AddProduct(_product);
        }

        [TestMethod]
        public void then_the_store_should_contain_the_product()
        {
            Assert.IsTrue(_store.Products.Contains(_product));
        }

        [TestMethod]
        public void then_the_product_should_be_available_in_the_store()
        {
            Assert.IsTrue(_product.StoresStockedIn.Contains(_store));
        }

        [TestMethod]
        public void then_the_product_should_have_one_store_in_his_StoresStockedIn_collection()
        {
            int count = 0;
            foreach (var store in _product.StoresStockedIn)
            {
                Assert.AreSame(_store, store);
                count++;
            }
            Assert.AreEqual(1, count);
        }
    }
}