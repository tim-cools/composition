using System;

namespace Soloco.Composition
{
    /// <summary>
    /// Unity Composition extension.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class CompositeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the types of the part implementations.
        /// </summary>
        /// <value>The type.</value>
        public Type [] Types { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAttribute"/> class.
        /// </summary>
        public CompositeAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAttribute"/> class.
        /// </summary>
        /// <param name="types">The types.</param>
        public CompositeAttribute(params Type[] types)
        {
            if (types == null) throw new ArgumentNullException("types");

            Types = types;
        }
    }
}