using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Soloco.Composition.Construction;
using Soloco.Composition.Generation;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public abstract class CompositionFactoryBuilderSpecification<TComposition> : UnitSpecification
        where TComposition : class 
    {
        private ICompositeFactory _compositeFactory;

        public Mock<IThisInjector> ThisInjectorMock { get; private set; }
        public Mock<IKnownPartTypesResolver> KnownPartTypesResolverMock { get; private set; }
        public Mock<ICompositePartsFactory> CompositePartsFactoryMock { get; private set; }
        public CompositeParts CompositeParts { get; private set; }
        public Mock<IInstanceResolver> InstanceResolverMock { get; private set; }
        public TComposition Result { get; private set; }

        protected override void Arrange()
        {
            ThisInjectorMock = new Mock<IThisInjector>();
            InstanceResolverMock = new Mock<IInstanceResolver>();
            CompositeParts = new CompositeParts();
            CompositePartsFactoryMock = new Mock<ICompositePartsFactory>();
            KnownPartTypesResolverMock = new Mock<IKnownPartTypesResolver>();

            InstanceResolverMock
                .Setup(i => i.Resolve(typeof(ICompositePartsFactory)))
                .Returns(CompositePartsFactoryMock.Object);
            InstanceResolverMock
                .Setup(i => i.Resolve(typeof(IThisInjector)))
                .Returns(ThisInjectorMock.Object);
            InstanceResolverMock
                .Setup(i => i.Resolve(typeof(IKnownPartTypesResolver)))
                .Returns(KnownPartTypesResolverMock.Object);
            KnownPartTypesResolverMock
                .Setup(r => r.Resolve(It.IsAny<Type>(), It.IsAny<IEnumerable<Type>>()))
                .Returns(new KnownPartTypes());
            CompositePartsFactoryMock
                .Setup(f => f.Create(It.IsAny<Type>(), It.IsAny<IEnumerable<Type>>(), It.IsAny<KnownPartTypes>(), It.IsAny<object>()))
                .Returns(CompositeParts);
            
            base.Arrange();
        }

        private object IEnumerable<T1>()
        {
            throw new NotImplementedException();
        }

        protected override void Act()
        {
            var builder = new CompositionFactoryBuilder(InstanceResolverMock.Object);
            _compositeFactory = builder.Create(typeof(TComposition));

            Result = _compositeFactory.Create() as TComposition;
        }

        public void VerifyResultNotNull()
        {
            Assert.IsNotNull(Result);
        }
    }
}