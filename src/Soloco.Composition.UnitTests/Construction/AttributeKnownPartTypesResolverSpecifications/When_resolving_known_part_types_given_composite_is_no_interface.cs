using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.AttributeKnownPartTypesResolverSpecifications
{
    [TestClass]
    public class When_resolving_known_part_types_given_composite_is_no_interface
    {
        public class Composite
        {
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Type TC.Unity.Composition.Tests.Construction.AttributeKnownPartTypesResolverSpecificationsWhen_resolving_known_part_types_given_composite_is_no_interface+Composite is no interface")]
        public void then_an_invalid_operation_exception_should_be_thrown()
        {
            var resolver = new AttributeKnownPartTypesResolver();
            resolver.Resolve(typeof (Composite), new List<Type>());
        } 
    }
}