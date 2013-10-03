using System;
using System.Text;

namespace Soloco.Composition.Generation
{
    internal class CodeClassNameFormatter
    {
        private readonly Type _type;

        public CodeClassNameFormatter(Type type)
        {
            _type = type;
        }

        public string GetFullClassName()
        {
            return GetClassPrefix() + "." + GetInterfaceName();
        }

        public string GetClassPrefix()
        {
            var nestedClassNames = new NestedClassNameFormatter(_type).Get();
            if (nestedClassNames.Length > 0)
            {
                return _type.Namespace + "." + nestedClassNames;
            }
            return _type.Namespace;                
        }

        private string GetInterfaceName()
        {
            return _type.IsGenericType
                 ? GetGenericClassName() 
                 : _type.Name;    
        }

        private string GetGenericClassName()
        {
            var genericType = _type.GetGenericTypeDefinition();
            var name = genericType.Name;

            //Some silly hack because the original generic type name
            //without the ` symbol is not provided by the 
            //MS reflection engine
            var separatorIndex = name.IndexOf('`');
            if (separatorIndex > 0)
            {
                name = name.Substring(0, separatorIndex);
            }
            return name + "<" + GetGenericArguments() + ">";
        }

        private string GetGenericArguments()
        {
            var builder = new StringBuilder();
            //var typeDefinition = _interfaceType.GetGenericTypeDefinition();
            foreach (var type in _type.GetGenericArguments())
            {
                if (builder.Length > 0)
                {
                    builder.Append(", ");
                }
                builder.Append(type.FullName);
            }
            return builder.ToString();
        }
    }
}