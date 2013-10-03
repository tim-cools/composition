using System;
using System.Linq;
using System.Reflection;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// 
    /// </summary>
    public class KnownPartType
    {
        internal Type[] Interfaces { get; private set; }
        internal Type Implementation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownPartType"/> class.
        /// </summary>
        /// <param name="interfaces">The interfaces.</param>
        /// <param name="implementation">The implementation.</param>
        internal KnownPartType(Type[] interfaces,Type implementation)
        {
            if (interfaces == null) throw new ArgumentNullException("interfaces");
            if (implementation == null) throw new ArgumentNullException("implementation");

            Interfaces = interfaces;
            Implementation = implementation;
        }
    }
}