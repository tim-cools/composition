using System;

namespace Soloco.Composition
{
    /// <summary>
    /// Defines the type of the composition imlpementation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class ImplementationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the implementation.
        /// </summary>
        /// <value>The type.</value>
        public Type ImplementationType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplementationAttribute"/> class.
        /// </summary>
        /// <param name="implementationType">The type.</param>
        public ImplementationAttribute(Type implementationType)
        {
            if (implementationType == null) throw new ArgumentNullException("implementationType");

            ImplementationType = implementationType;
        }
    }
}