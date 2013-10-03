namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public class ParentParentNested
    {
        public class ParentNested
        {
            public interface IDoubleNestedPart
            {
                void Test();
            }

            public interface IDoubleNestedSingleComposition : IDoubleNestedPart
            {
            }
        }
    }
}