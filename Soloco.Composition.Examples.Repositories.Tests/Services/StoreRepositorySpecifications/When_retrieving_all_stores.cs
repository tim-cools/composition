using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.StoreRepositorySpecifications
{
    public class When_retrieving_all_stores
    {
        [TestClass]
        public class given_some_stores_are_in_the_database : StoreRepositorySpecification
        {
            private IEnumerable<Store> _storesActual;
            private IEnumerable<Store> _storesExpected;

            protected override void Arrange()
            {
                base.Arrange();

                _storesExpected = TestStoreFactory.CreateList();
                Session.PersistAll(_storesExpected);
            }

            protected override void Act()
            {
                _storesActual = Repository.RetrieveAll();
            }

            [TestMethod]
            public void then_all_the_stores_should_be_returned()
            {
                Assert.IsNotNull(_storesActual);
                Assert.AreEqual(2, _storesActual.Count());

                foreach (var actual in _storesActual)
                {
                    var expected = _storesExpected.SingleOrDefault(c => c.Id == actual.Id);
                    Assert.AreEqual(actual, expected);
                }
            }
        }

        [TestClass]
        public class given_no_stores_are_in_the_database : StoreRepositorySpecification
        {
            private IEnumerable<Store> _storesActual;

            protected override void Act()
            {
                _storesActual = Repository.RetrieveAll();
            }

            [TestMethod]
            public void then_no_the_stores_should_be_returned()
            {
                Assert.IsNotNull(_storesActual);
                Assert.AreEqual(0, _storesActual.Count());
            }
        }
    }
}