using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.ThisFieldResolverSpecifications
{
    public class MultiplThisFieldObject
    {
        [This]
        private Child _child1;
        [This]
        private Child _child2;
        [This]
        private Child _child3;
    }
}