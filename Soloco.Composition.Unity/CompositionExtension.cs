using System.Security.Permissions;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Soloco.Composition.Construction;
using Soloco.Composition.Generation;
using Soloco.Composition.InstanceResolver;
using System;

namespace Soloco.Composition.Unity
{
    /// <summary>
    /// Unity Composition extension.
    /// </summary>
    public class CompositionExtension : UnityContainerExtension
    {
        private volatile ICompositionFactoryBuilder _builder;
        private readonly object _builderLock = new object();

        private ICompositionFactoryBuilder Builder
        {
            get
            {
                if (_builder == null)
                {
                    lock (_builderLock)
                    {
                        if (_builder == null)
                        {
                            _builder = Container.Resolve<ICompositionFactoryBuilder>();
                        }
                    }
                }
                return _builder;
            }
        }

        /// <summary>
        /// Initial the container with this extension's functionality.
        /// </summary>
        /// <remarks>
        /// When overridden in a derived class, this method will modify the given
        /// <see cref="ExtensionContext"/> by adding strategies, policies, etc. to
        /// install it's functions into the container.</remarks>
        protected override void Initialize()
        {
            Container
                .RegisterType<CompositionPolicy>()
                .RegisterType<ICompositionFactoryBuilder, CompositionFactoryCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IThisInjector, ThisInjector>(new ContainerControlledLifetimeManager())
                .RegisterType<ICompositePartsFactory, CompositePartsFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IKnownPartTypesResolver, AttributeKnownPartTypesResolver>(new ContainerControlledLifetimeManager())
                .RegisterInstance<IInstanceResolver>(new UnityInstanceResolver(Container));
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        public CompositionExtension RegisterComposite<TComposite>()
        {
            return RegisterComposite(typeof(TComposite));
        }

        /// <summary>
        /// Registers a composite type in the container.
        /// </summary>
        public CompositionExtension RegisterComposite(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            SetBuildPlanPolicy(type);
            return this;
        }

        private void SetBuildPlanPolicy(Type type)
        {
            var policy = Container.Resolve<CompositionPolicy>();
            var key = new NamedTypeBuildKey(type);
            
            Context.Policies.Set<IBuildPlanPolicy>(policy, key);
        }

        /// <summary>
        /// Composes the specified composite type with an existing object as part of the composit.
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public TResult Compose<TResult>()
        {
            var compositionFactory = Builder.Create(typeof(TResult));
            return (TResult) compositionFactory.Create();
        }

        /// <summary>
        /// Composes the specified composite type with an existing object as part of the composit.
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public TResult Compose<TResult>(object existing)
        {
            if (existing == null) throw new ArgumentNullException("existing");
            
            var compositionFactory = Builder.Create(typeof (TResult));
            return (TResult) compositionFactory.Compose(existing);
        }
    }
}