using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using TC.Unity.Composition.UnitTests.Dependencies;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public class When_a_composition_is_created_given_an_interface_with_external_dependencies
    {
        [TestClass]
        public class as_property_type :
            CompositionFactoryBuilderSpecification<as_property_type.IComposition>
        {
            public interface IPart
            {
                SomeArgumentType Test { get; set; }
            }

            public interface IComposition : IPart
            {
            }

            private readonly SomeArgumentType _returnValue = new SomeArgumentType { SomeValue = "Some Value" };
            private Mock<IPart> _partMock;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new Mock<IPart>();

                CompositeParts.Add(typeof(IPart), _partMock.Object);

                _partMock
                    .SetupGet(p => p.Test)
                    .Returns(_returnValue);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }

            [TestMethod]
            public void then_the_property_should_be_gettable()
            {
                Assert.AreSame(_returnValue, Result.Test);
                _partMock.VerifyGet(p => p.Test);
            }

            [TestMethod]
            public void then_the_property_should_be_settable()
            {
                Result.Test = _returnValue;
                _partMock.VerifySet(p => p.Test = _returnValue);
            }
        }

        [TestClass]
        public class as_method_parameter_type :
            CompositionFactoryBuilderSpecification<as_method_parameter_type.IComposition>
        {
            public interface IPart
            {
                void Test(SomeArgumentType parameter);
            }

            public interface IComposition : IPart
            {
            }

            private readonly SomeArgumentType _returnValue = new SomeArgumentType { SomeValue = "Some Value" };
            private Mock<IPart> _partMock;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new Mock<IPart>();

                CompositeParts.Add(typeof(IPart), _partMock.Object);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }

            [TestMethod]
            public void then_the_method_should_be_callable()
            {
                Result.Test(_returnValue);
                _partMock.Verify(p => p.Test(_returnValue));
            }
        }

        [TestClass]
        public class as_method_return_type :
            CompositionFactoryBuilderSpecification<as_method_return_type.IComposition>
        {
            public interface IPart
            {
                SomeArgumentType Test();
            }

            public interface IComposition : IPart
            {
            }

            private readonly SomeArgumentType _returnValue = new SomeArgumentType { SomeValue = "Some Value" };
            private Mock<IPart> _partMock;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new Mock<IPart>();

                CompositeParts.Add(typeof(IPart), _partMock.Object);

                _partMock
                    .Setup(p => p.Test())
                    .Returns(_returnValue);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }

            [TestMethod]
            public void then_the_method_should_be_callable()
            {
                Assert.AreSame(_returnValue, Result.Test());
                _partMock.Verify(p => p.Test());
            }
        }

        [TestClass]
        public class as_event_handler_type :
            CompositionFactoryBuilderSpecification<as_event_handler_type.IEventComposition>
        {
            public interface IEventPart
            {
                event SomeEventHandler SimpleEvent;
            }

            class EventPartMock : IEventPart
            {
                public event SomeEventHandler SimpleEvent
                {
                    add
                    {
                        SimpleEventHandler = value;
                        SimpleEventAdded = true;
                    }
                    remove
                    {
                        SimpleEventHandler = value;
                        SimpleEventRemoved = true;
                    }
                }

                public SomeEventHandler SimpleEventHandler { get; private set; }

                public bool SimpleEventAdded { get; private set; }
                public bool SimpleEventRemoved { get; private set; }

                internal void RaiseSimpleEvent()
                {
                    if (SimpleEventHandler == null)
                    {
                        throw new InvalidOperationException("SimpleEventHandler == null");
                    }
                    SimpleEventHandler();
                }
            }

            public interface IEventComposition : IEventPart
            {
            }

            private EventPartMock _partMock;
            private bool _simpleEventRaised;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new EventPartMock();

                CompositeParts.Add(typeof(IEventPart), _partMock);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }

            [TestMethod]
            public void then_the_simple_event_should_subscribable()
            {
                SomeEventHandler handler = Result_SimpleEvent;
                Result.SimpleEvent += handler;

                Assert.AreSame(handler, _partMock.SimpleEventHandler);
                Assert.IsTrue(_partMock.SimpleEventAdded);
            }

            [TestMethod]
            public void then_the_simple_event_should_unsubscribable()
            {
                SomeEventHandler handler = Result_SimpleEvent;
                Result.SimpleEvent -= handler;

                Assert.AreSame(handler, _partMock.SimpleEventHandler);
                Assert.IsTrue(_partMock.SimpleEventRemoved);
            }

            [TestMethod]
            public void then_subscriber_of_the_simple_event_should_called()
            {
                SomeEventHandler handler = Result_SimpleEvent;
                Result.SimpleEvent += handler;
                _partMock.RaiseSimpleEvent();

                Assert.IsTrue(_simpleEventRaised);
            }

            void Result_SimpleEvent()
            {
                _simpleEventRaised = true;
            }
        }

        [TestClass]
        public class as_composition_part :
            CompositionFactoryBuilderSpecification<as_composition_part.IComposition>
        {
            public interface IComposition : ISomePart
            {
            }

            private Mock<ISomePart> _partMock;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new Mock<ISomePart>();

                CompositeParts.Add(typeof(ISomePart), _partMock.Object);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }
        }

        [TestClass]
        public class as_composition_base_part :
            CompositionFactoryBuilderSpecification<as_composition_base_part.IComposition>
        {
            public interface IParentSomePart : ISomePart
            {

            }
            public interface IComposition : IParentSomePart
            {
            }

            private Mock<IParentSomePart> _partMock;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new Mock<IParentSomePart>();

                CompositeParts.Add(typeof(IParentSomePart), _partMock.Object);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }
        }

        [TestClass]
        public class as_composition :
            CompositionFactoryBuilderSpecification<ISomeComposition>
        {
            private Mock<ISomePart> _partMock;

            protected override void Arrange()
            {
                base.Arrange();

                _partMock = new Mock<ISomePart>();

                CompositeParts.Add(typeof(ISomePart), _partMock.Object);
            }

            [TestMethod]
            public void then_the_constucted_type_should_not_be_null()
            {
                VerifyResultNotNull();
            }
        }
    }
}