using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.ProductRepositorySpecifications
{
    public class When_counting_all_products
    {
        [TestClass]
        public class given_there_are_products_in_the_database : ProductRepositorySpecification
        {
            private int _countActual;
            private int _countExpected;

            protected override void Arrange()
            {
                base.Arrange();

                var products = TestProductFactory.CreateList();
                _countExpected = products.Count();
                Session.PersistAll(products);
            }

            protected override void Act()
            {
                _countActual = Repository.Count();
            }

            [TestMethod]
            public void then_all_the_products_should_be_returned()
            {
                Assert.AreEqual(_countExpected, _countActual);
            }
        }

        [TestClass]
        public class given_no_products_are_in_the_database : ProductRepositorySpecification
        {
            private long _countActual;
            
            protected override void Act()
            {
                _countActual = Repository.Count();
            }

            [TestMethod]
            public void then_all_the_products_should_be_returned()
            {
                Assert.AreEqual(0, _countActual);
            }
        }
    }
}