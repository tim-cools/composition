using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Examples.Repositories.Tests.DatabaseBasedSpecificationTest
{
    [TestClass]
    public class When_using_a_database_based_specification : DatabaseBasedSpecification
    {
        protected override void Act()
        {
        }

        [TestMethod]
        public void then_we_should_have_a_container()
        {
            Assert.IsNotNull(Container);
        }
        
        [TestMethod]
        public void then_we_should_have_a_session()
        {
            Assert.IsNotNull(Session);
        }
    }
}