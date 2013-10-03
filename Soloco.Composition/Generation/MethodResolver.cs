using System;
using System.Collections.Generic;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class MethodResolver
    {
        private readonly Type _interface;

        public MethodResolver(Type @interface)
        {
            if (@interface == null) throw new ArgumentNullException("interface");

            _interface = @interface;
        }

        public IEnumerable<MethodInfo> Resolve()
        {
            var result = new List<MethodInfo>();
            Resolve(result, _interface);
            return result;
        }

        private static void Resolve(ICollection<MethodInfo> result, Type @interface)
        {
            AddMethods(@interface, result);
            foreach (var baseInterface in @interface.GetInterfaces())
            {
                Resolve(result, baseInterface);
            }
        }

        private static void AddMethods(Type @interface, ICollection<MethodInfo> result)
        {
            foreach (var method in @interface.GetMethods())
            {
                AddMethod(method, result);
            }
        }

        private static void AddMethod(MethodInfo method, ICollection<MethodInfo> result)
        {
            if (method.IsSpecialName) return;

            result.Add(method);
        }
    }
}