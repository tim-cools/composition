using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories.Services;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.EmployeeRepositorySpecifications
{
    [TestClass]
    public abstract class EmployeeRepositorySpecification : RepositorySpecification<IEmployeeRepository>
    {
    }
}
