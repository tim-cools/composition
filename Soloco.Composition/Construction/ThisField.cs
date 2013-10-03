
using System;
using System.Globalization;
using System.Reflection;

namespace Soloco.Composition.Construction
{
    internal class ThisField
    {
        private readonly FieldInfo _field;
        private readonly ThisFieldSetter _thisFieldSetter;

        public FieldInfo Field
        {
            get { return _field; }
        }

        public ThisField(FieldInfo field)
        {
            if (field == null) throw new ArgumentNullException("field");

            _field = field;
            _thisFieldSetter = ThisFieldSetterGenerator.Generate(field);
        }

        public void Inject(object parent, CompositeParts mapping)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (mapping == null) throw new ArgumentNullException("mapping");

            var thisValue = mapping[_field.FieldType];

            ThrowThisInjectionFailedExceptionWhenValueNotFound(_field, thisValue);

            _thisFieldSetter(parent, thisValue);
        }

        private static void ThrowThisInjectionFailedExceptionWhenValueNotFound(FieldInfo fieldInfo, object thisValue)
        {
            if (thisValue != null)
            {
                return;
            }
            
            throw new InvalidOperationException(
                string.Format(CultureInfo.CurrentCulture,
                              "Could not inject 'this' into field '{0}' of type '{1}'{2}No value found of type '{3}'",
                              fieldInfo.Name,
                              fieldInfo.DeclaringType.FullName,
                              Environment.NewLine,
                              fieldInfo.FieldType));
        }
    }
}