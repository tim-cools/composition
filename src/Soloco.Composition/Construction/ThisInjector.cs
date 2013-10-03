
using System;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Resolves and injects self objects in fields decorated by the [this] attribute.
    /// </summary>
    public class ThisInjector : IThisInjector
    {
        private readonly ThisFieldResolver _resolver = new ThisFieldResolver();

        /// <summary>
        /// Resolves and injects self objects in fields decorated by the [this] attribute.
        /// </summary>
        public void Inject(CompositeParts parts)
        {
            if (parts == null) throw new ArgumentNullException("parts");

            foreach (var value in parts.GetInstances())
            {
                Inject(parts, value);
            }
        }

        private void Inject(CompositeParts compositeParts, object value)
        {
            if (value == null) throw new ArgumentNullException("value");

            var fields = _resolver.Resolve(value.GetType());
            foreach (var field in fields)
            {
                field.Inject(value, compositeParts);
            }
        }
    }
}