using System;
using System.Globalization;
using System.Text;

namespace Soloco.Composition.Generation
{
    internal class CodeCompositionClassNameFormatter
    {
        private readonly Type _type;
        private readonly string _typeNname;

        public CodeCompositionClassNameFormatter(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            _type = type;
            _typeNname = _type.Name;
        }

        public string GetClassName()
        {
            var name = GetGenericClassName();

            if (name.Length > 1
             && name.StartsWith("I", StringComparison.Ordinal))
            {
                return name.Substring(1);
            }
            return name + "Composition";
 
        }
        public string GetGenericClassName()
        {
            var name = _typeNname;

            //Some silly hack because the original generic type name
            //without the ` symbol is not provided by the 
            //MS reflection engine
            var separatorIndex = name.IndexOf('`');
            if (separatorIndex > 0)
            {
                name = name.Substring(0, separatorIndex);
            }

            return _type.IsGenericType
                 ? AddGenericClassNameArguments(name) 
                 : name;
        }

        public string GetClassPrefix()
        {
            var nestedClassNames = new NestedClassNameFormatter(_type).Get();
            if (nestedClassNames.Length > 0)
            {
                return _type.Namespace + ".Nested." + nestedClassNames;
            }
            return _type.Namespace;
        }

        private string AddGenericClassNameArguments(string name)
        {
            var builder = new StringBuilder();
            builder.Append(name);
            builder.Append("Of");
            builder.Append(AddGenericClassNameArguments());
            return builder.ToString();
        }

        private string AddGenericClassNameArguments()
        {
            var builder = new StringBuilder();
            foreach (var argumentType in _type.GetGenericArguments())
            {
                if (builder.Length > 0)
                {
                    builder.Append("And");
                }
                var formatter = new CodeCompositionClassNameFormatter(argumentType);
                builder.Append(formatter.GetGenericClassName());
            }
            return builder.ToString();
        }
    }
}