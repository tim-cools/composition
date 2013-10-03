using System;
using System.Collections.Generic;

namespace Soloco.Composition.Generation
{
    internal class GenerationContext
    {
        public IEnumerable<Type> PartInterfaces { get; set; }
        public Type CompositionType { get; private set; }
        public Type ComposedType { get; set; }

        public string ClassNamespace
        {
            get
            {
                return new CodeCompositionClassNameFormatter(CompositionType).GetClassPrefix();
            }
        }

        public string ClassFullName
        {
            get
            {
                return ClassNamespace + "." + ClassName;
            }
        }

        public string ClassName
        {
            get
            {
                return new CodeCompositionClassNameFormatter(CompositionType).GetClassName();
            }
        }
        
        public GenerationContext(Type type)
        {
            CompositionType = type;
        }
    }
}