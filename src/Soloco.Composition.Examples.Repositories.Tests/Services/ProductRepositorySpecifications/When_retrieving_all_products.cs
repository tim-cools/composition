using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.ProductRepositorySpecifications
{
    public class When_retrieving_all_products
    {
        [TestClass]
        public class given_some_products_are_in_the_database : ProductRepositorySpecification
        {
            private IEnumerable<Product> _productsActual;
            private IEnumerable<Product> _productsExpected;

            protected override void Arrange()
            {
                base.Arrange();

                _productsExpected = TestProductFactory.CreateList();
                Session.PersistAll(_productsExpected);
            }

            protected override void Act()
            {
                _productsActual = Repository.RetrieveAll();
            }

            [TestMethod]
            public void then_all_the_products_should_be_returned()
            {
                Assert.IsNotNull(_productsActual);
                Assert.AreEqual(2, _productsActual.Count());

                foreach (var actual in _productsActual)
                {
                    var expected = _productsExpected.SingleOrDefault(c => c.Id == actual.Id);
                    Assert.AreEqual(actual, expected);
                }
            }
        }

        [TestClass]
        public class given_no_products_are_in_the_database : ProductRepositorySpecification
        {
            private IEnumerable<Product> _productsActual;

            protected override void Act()
            {
                _productsActual = Repository.RetrieveAll();
            }

            [TestMethod]
            public void then_no_the_products_should_be_returned()
            {
                Assert.IsNotNull(_productsActual);
                Assert.AreEqual(0, _productsActual.Count());
            }
        }
    }
}