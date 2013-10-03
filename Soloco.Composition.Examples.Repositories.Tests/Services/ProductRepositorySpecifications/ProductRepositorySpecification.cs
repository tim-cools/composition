using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories.Services;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.ProductRepositorySpecifications
{
    [TestClass]
    public abstract class ProductRepositorySpecification : RepositorySpecification<IProductRepository>
    {
    }
}
