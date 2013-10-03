using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;
using Soloco.Composition.Generation;

namespace Soloco.Composition.Unity
{
    /// <summary>
    /// A build plan used to create composits.
    /// </summary>
    public class CompositionPolicy : IBuildPlanPolicy
    {
        private readonly ICompositionFactoryBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionPolicy"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public CompositionPolicy(ICompositionFactoryBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Creates an instance of this build plan's type, or fills
        /// in the existing type if passed in.
        /// </summary>
        /// <param name="context">Context used to build up the object.</param>
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public void BuildUp(IBuilderContext context)
        {
            var type = context.BuildKey.Type;
            var compositionType = _builder.Create(type);

            context.Existing = compositionType.Create();
        }
    }
}