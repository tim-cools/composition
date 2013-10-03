using System;
using System.Collections.Generic;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class PropertyResolver
    {
        private readonly Type _interface;

        public PropertyResolver(Type @interface)
        {
            if (@interface == null) throw new ArgumentNullException("interface");

            _interface = @interface;
        }

        public IEnumerable<PropertyInfo> Resolve()
        {
            var result = new List<PropertyInfo>();
            Resolve(result, _interface);
            return result;
        }

        private static void Resolve(ICollection<PropertyInfo> result, Type @interface)
        {
            AddMethods(@interface, result);
            foreach (var baseInterface in @interface.GetInterfaces())
            {
                Resolve(result, baseInterface);
            }
        }

        private static void AddMethods(Type @interface, ICollection<PropertyInfo> result)
        {
            foreach (var property in @interface.GetProperties())
            {
                result.Add(property);
            }
        }
    }
}