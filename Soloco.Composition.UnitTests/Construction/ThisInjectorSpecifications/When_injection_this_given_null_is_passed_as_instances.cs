using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.ThisInjectorSpecifications
{
    [TestClass]
    public class When_injection_this_given_null_is_passed_as_instances
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void then_a_argument_null_exception_shoul_be_thrown()
        {
            var injector = new ThisInjector();
            injector.Inject(null);
        }
    }
}