using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.ThisFieldResolverSpecifications
{
    public class SingleThisFieldObject
    {
        [This]
        private Child _child1;
        private Child _child2;
        private Child _child3;
    }
}