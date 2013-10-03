using System;
using System.Collections.Generic;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class EventResolver
    {
        private readonly Type _interface;

        public EventResolver(Type @interface)
        {
            if (@interface == null) throw new ArgumentNullException("interface");

            _interface = @interface;
        }

        public IEnumerable<EventInfo> Resolve()
        {
            var result = new List<EventInfo>();
            Resolve(result, _interface);
            return result;
        }

        private static void Resolve(ICollection<EventInfo> result, Type @interface)
        {
            AddMethods(@interface, result);
            foreach (var baseInterface in @interface.GetInterfaces())
            {
                Resolve(result, baseInterface);
            }
        }

        private static void AddMethods(Type @interface, ICollection<EventInfo> result)
        {
            foreach (var property in @interface.GetEvents())
            {
                result.Add(property);
            }
        }
    }
}