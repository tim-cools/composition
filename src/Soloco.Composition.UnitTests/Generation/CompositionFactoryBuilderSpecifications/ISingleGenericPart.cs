namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface ISingleGenericPart<T>
    {
        void TestGenericParameter(T value);
        T TestGenericReturnValue();
        T TestGenericReturnValueAndParameter(T value);
        T TestGenericReturnValueAndParameterAndAnotherGenericParameter<TZ>(T valueT, TZ valueZ);
        TZ TestGenericReturnValueAndParameterAndAnotherGenericReturnValue<TZ>(T valueT);
    }
}