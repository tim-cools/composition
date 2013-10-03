using System.Globalization;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

using System;

namespace Soloco.Composition.UnitTests.Construction.AttributeKnownPartTypesResolverSpecifications
{
    [TestClass]
    public class When_resolving_known_part_types_based_on_attributes
    {
        public interface IPart1
        {
        }

        public interface IPart2
        {
        }

        [Implementation(typeof(PartOverridenPart1))]
        public interface IOverridenPart1 : IPart1
        {
        }

        [Implementation(typeof(PartOverridenPart2))]
        public interface IOverridenPart2 : IPart2
        {
        }

        public class CompositeOverridenPart1 : IPart1
        {
        }

        public class CompositeOverridenPart2 : IPart2
        {
        }

        public class PartOverridenPart1 : IOverridenPart1
        {
        }

        public class PartOverridenPart2 : IOverridenPart2
        {
        }

        public class ResolvingKnownPartTypesContext
        {
            public Type Part1OverrideType { get; private set; }
            public Type Part2OverrideType { get; private set; }
            public Type CompositeType { get; private set; }

            public ResolvingKnownPartTypesContext(Type part1OverrideType, Type part2OverrideType, Type compositeType)
            {
                Part1OverrideType = part1OverrideType;
                Part2OverrideType = part2OverrideType;
                CompositeType = compositeType;
            }
        }

        public interface IComposite : IPart1, IPart2
        {
        }

        public interface ICompositePart2Part : IPart1, IOverridenPart2
        {
        }

        public interface ICompositePart1Part : IOverridenPart1, IPart2
        {
        }

        public interface ICompositePart1Part2Part : IOverridenPart1, IOverridenPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1))]
        public interface ICompositePart1Composite : IPart1, IPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1))]
        public interface ICompositePart1CompositePart2Part : IPart1, IOverridenPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1))]
        public interface ICompositePart1CompositePart1Part : IOverridenPart1, IPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1))]
        public interface ICompositePart1CompositePart1Part2Part : IOverridenPart1, IOverridenPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart2))]
        public interface ICompositePart2Composite : IPart1, IPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart2))]
        public interface ICompositePart2CompositePart2Part : IPart1, IOverridenPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart2))]
        public interface ICompositePart2CompositePart1Part : IOverridenPart1, IPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart2))]
        public interface ICompositePart2CompositePart1Part2Part : IOverridenPart1, IOverridenPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2))]
        public interface ICompositePart1Part2Composite : IPart1, IPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2))]
        public interface ICompositePart1Part2CompositePart2Part : IPart1, IOverridenPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2))]
        public interface ICompositePart1Part2CompositePart1Part : IOverridenPart1, IPart2
        {
        }

        [Composite(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2))]
        public interface ICompositePart1Part2CompositePart1Part2Part : IOverridenPart1, IOverridenPart2
        {
        }

        private readonly ResolvingKnownPartTypesContext[] _contexts = new[]
        {
            new ResolvingKnownPartTypesContext(null, null, typeof(IComposite)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), null, typeof(ICompositePart1Composite)), 
            new ResolvingKnownPartTypesContext(null, typeof(CompositeOverridenPart2), typeof(ICompositePart2Composite)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2), typeof(ICompositePart1Part2Composite)), 
            new ResolvingKnownPartTypesContext(typeof(PartOverridenPart1), null, typeof(ICompositePart1Part)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), null, typeof(ICompositePart1CompositePart1Part)), 
            new ResolvingKnownPartTypesContext(typeof(PartOverridenPart1), typeof(CompositeOverridenPart2), typeof(ICompositePart2CompositePart1Part)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2), typeof(ICompositePart1Part2CompositePart1Part)), 
            new ResolvingKnownPartTypesContext(null, typeof(PartOverridenPart2), typeof(ICompositePart2Part)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), typeof(PartOverridenPart2), typeof(ICompositePart1CompositePart2Part)), 
            new ResolvingKnownPartTypesContext(null, typeof(CompositeOverridenPart2), typeof(ICompositePart2CompositePart2Part)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2), typeof(ICompositePart1Part2CompositePart2Part)), 
            new ResolvingKnownPartTypesContext(typeof(PartOverridenPart1), typeof(PartOverridenPart2), typeof(ICompositePart1Part2Part)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), typeof(PartOverridenPart2), typeof(ICompositePart1CompositePart1Part2Part)), 
            new ResolvingKnownPartTypesContext(typeof(PartOverridenPart1), typeof(CompositeOverridenPart2), typeof(ICompositePart2CompositePart1Part2Part)), 
            new ResolvingKnownPartTypesContext(typeof(CompositeOverridenPart1), typeof(CompositeOverridenPart2), typeof(ICompositePart1Part2CompositePart1Part2Part)), 
        };

        [TestMethod]
        public void then_known_part_types_should_contain_correct_overrides()
        {
            var messageBuilder = new StringBuilder();
            foreach (var context in _contexts)
            {
                VerifyContext(context, messageBuilder);
            }
            VerifyMessageBuilder(messageBuilder);
        }

        private static void VerifyMessageBuilder(StringBuilder messageBuilder)
        {
            if (messageBuilder.Length > 0)
            {
                Assert.Fail(messageBuilder.ToString());
            }
        }

        private static void VerifyContext(ResolvingKnownPartTypesContext context, StringBuilder builder)
        {
            var compositeType = context.CompositeType;
            try
            {
                var resolver = new AttributeKnownPartTypesResolver();
                var interfaces = compositeType.GetInterfaces();
                var knownPartTypes = resolver.Resolve(compositeType, interfaces);

                AssertPart(typeof(IPart1), compositeType, context.Part1OverrideType, knownPartTypes, builder);
                AssertPart(typeof(IPart2), compositeType, context.Part2OverrideType, knownPartTypes, builder);
            }
            catch (Exception ex)
            {
                builder.AppendLine(
                    string.Format(CultureInfo.InvariantCulture,
                                  "Exception thrown when checking for composite type {0}.{1}{2}",
                                  compositeType,
                                  Environment.NewLine,
                                  ex
                        ));
            }
        }

        private static void AssertPart(Type partType, Type compositeType, Type expected, KnownPartTypes knownPartTypes, StringBuilder builder)
        {
            var message = AssertPart(partType, compositeType, expected, knownPartTypes);
            if (message != null)
            {
                builder.AppendLine(message);
            }
        }

        private static string AssertPart(Type partType, Type compositeType, Type expected, KnownPartTypes knownPartTypes)
        {
            var actual = knownPartTypes.GetImplementationType(partType);
            var title = partType.Name;

            return expected == null
                       ? AssertPartNull(compositeType, actual, title)
                       : AssertEqual(compositeType, expected, actual, title);
        }

        private static string AssertEqual(Type compositeType, Type expected, Type actual, string title)
        {
            if (actual == null)
            {
                return string.Format(
                    CultureInfo.InvariantCulture, 
                    "Failure when checking {0} for composite type {1}. Part type should be '{2}' but is 'null'", 
                    title, compositeType, expected.FullName);
            }

            if (actual != expected)
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Failure when checking {0} for composite type {1}. Part type should be '{2}' but is '{3}'",
                    title, compositeType, expected.FullName, actual.GetType().FullName);
            }

            return null;
        }

        private static string AssertPartNull(Type compositeType, Type actual, string title)
        {
            if (actual == null) return null;

            return string.Format(
                CultureInfo.InvariantCulture,
                "Failure when checking {0} for composite type {1}. Part type should be 'null' but is '{2}'",
                title, compositeType, actual.GetType().FullName);
        }
    }
}