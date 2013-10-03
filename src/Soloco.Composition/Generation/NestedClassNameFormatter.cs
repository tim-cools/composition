using System;
using System.Text;

namespace Soloco.Composition.Generation
{
    internal class NestedClassNameFormatter
    {
        private readonly Type _type;

        public NestedClassNameFormatter(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            
            _type = type;
        }

        public string Get()
        {
            var declaringType = _type.DeclaringType;
            if (declaringType == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            while (declaringType != null)
            {
                if (builder.Length > 0)
                {
                    builder.Insert(0, ".");
                }
                builder.Insert(0, declaringType.Name);

                declaringType = declaringType.DeclaringType;
            }
            return builder.ToString();
        }
    }
}