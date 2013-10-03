using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_empty_composition_is_used_with_non_default_name :
        CompositionFactoryBuilderSpecification<EmptyComposition>
    {
        [TestMethod]
        public void then_the_composition_should_implement_the_composition_interface()
        {
            VerifyResultNotNull();
        }
    }
}