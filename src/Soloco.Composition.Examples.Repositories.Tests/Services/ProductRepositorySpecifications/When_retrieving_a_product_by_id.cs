using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.ProductRepositorySpecifications
{
    public class When_retrieving_a_product_by_id
    {
        [TestClass]
        public class given_the_product_is_in_the_database : ProductRepositorySpecification
        {
            private Product _productsActual;
            private Product _productsExpected;

            protected override void Arrange()
            {
                base.Arrange();

                _productsExpected = TestProductFactory.Create();
                Session.Persist(_productsExpected);
            }

            protected override void Act()
            {
                _productsActual = Repository.RetrieveById(_productsExpected.Id);
            }

            [TestMethod]
            public void then_the_product_should_be_returned()
            {
                Assert.IsNotNull(_productsActual);
                Assert.AreEqual(_productsExpected, _productsActual);
            }
        }

        [TestClass]
        public class given_no_products_are_in_the_database : ProductRepositorySpecification
        {
            private Product _productActual;

            protected override void Act()
            {
                _productActual = Repository.RetrieveById(4);
            }

            [TestMethod]
            public void then_no_the_product_should_be_returned()
            {
                Assert.IsNull(_productActual);
            }
        }
    }
}