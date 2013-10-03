using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.Generation
{
    /// <summary>
    /// Cache for the composition factories
    /// </summary>
    public class CompositionFactoryCache : ICompositionFactoryBuilder
    {
        private readonly Dictionary<Type, ICompositeFactory> _cache = new Dictionary<Type, ICompositeFactory>();
        private readonly object _cacheLock = new object();
        private readonly IInstanceResolver _instanceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionFactoryCache"/> class.
        /// </summary>
        /// <param name="instanceResolver">The instance resolver.</param>
        public CompositionFactoryCache(IInstanceResolver instanceResolver)
        {
            if (instanceResolver == null) throw new ArgumentNullException("instanceResolver");

            _instanceResolver = instanceResolver;
        }

        /// <summary>
        /// Creates a composition factory for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public ICompositeFactory Create(Type type)
        {
            if (!_cache.ContainsKey(type))
            {
                lock (_cacheLock)
                {
                    if (!_cache.ContainsKey(type))
                    {
                        AddAndCreate(type);
                    }
                }
            }
            return _cache[type];
        }

        private void AddAndCreate(Type type)
        {
            var builder = new CompositionFactoryBuilder(_instanceResolver);
            _cache.Add(type, builder.Create(type));
        }
    }
}