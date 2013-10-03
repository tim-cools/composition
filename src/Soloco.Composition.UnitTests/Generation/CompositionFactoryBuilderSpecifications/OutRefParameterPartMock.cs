using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    class OutRefParameterPartMock : IOutRefParameterPart
    {
        private Exception _returnType;

        public bool TestOutCalled { get; private set; }
        public bool TestRefCalled { get; private set; }
        public Exception TestRefInValue { get; private set; }

        public OutRefParameterPartMock(Exception returnType)
        {
            _returnType = returnType;
        }

        public void TestOut(out Exception value)
        {
            value = _returnType;
            TestOutCalled = true;
        }

        public void TestRef(ref Exception value)
        {
            TestRefInValue = value;
            value = _returnType;
            TestRefCalled = true;
        }
    }
}