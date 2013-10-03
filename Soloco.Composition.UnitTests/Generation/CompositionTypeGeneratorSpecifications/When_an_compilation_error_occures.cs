using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Generation;

namespace Soloco.Composition.UnitTests.Generation.CompositionTypeGeneratorSpecifications
{
    [TestClass]
    public class When_an_compilation_error_occures
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), @"Error occured while compiling composed type 'TC.Unity.Composition.Tests.Generation.CompositionTypeGeneratorSpecifications.When_an_compilation_error_occures'")]
        public void then_an_exception_should_be_thrown()
        {
            var type = typeof(When_an_compilation_error_occures);
            var context = new GenerationContext(type);
            context.PartInterfaces = new List<Type> {typeof (When_an_compilation_error_occures)};
                var generator = new CompositionTypeGenerator(context);
            generator.Generate();
        }
    }
}