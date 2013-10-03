using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soloco.Composition.Example.Repositories.Services;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    [TestClass]
    public abstract class RepositorySpecification<T> : DatabaseBasedSpecification
        where T : IRepository
    {
        public T Repository { get; private set; }

        protected override void Arrange()
        {
            base.Arrange();

            Repository = Container.Compose<T>();
        }

        [TestMethod]
        public void then_the_repository_should_not_be_null()
        {
            Assert.IsNotNull(Repository);
        }
    }
}