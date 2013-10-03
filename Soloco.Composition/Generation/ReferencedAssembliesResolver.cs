using System;
using System.Collections.Generic;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class ReferencedAssembliesResolver
    {
        private readonly GenerationContext _context;
        private List<string> _result;

        public ReferencedAssembliesResolver(GenerationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        internal IEnumerable<string> Resolve()
        {
            _result = new List<string>();

            AddComposition();
            AddCompositionParts();
            AddMethodTypes();
            AddPropertyTypes();
            AddEventTypes();

            return _result;
        }   

        private void AddComposition()
        {
            AddReference(_context.CompositionType);
        }

        private void AddCompositionParts()
        {
            foreach (var type in _context.PartInterfaces)
            {
                AddReference(type);
            }
        }

        private void AddMethodTypes()
        {
            var resolver = new MethodResolver(_context.CompositionType);
            var methods = resolver.Resolve();
            foreach (var method in methods)
            {
                AddMethodTypes(method);
            }
        }

        private void AddMethodTypes(MethodInfo method)
        {
            AddReference(method.ReturnType);
            AddMethodParameterTypes(method);
            AddMethodGenericTypes(method);
        }   

        private void AddMethodParameterTypes(MethodInfo method)
        {
            foreach (var parameter in method.GetParameters())
            {
                AddReference(parameter.ParameterType);
            }
        }

        private void AddMethodGenericTypes(MethodBase method)
        {
            var arguments = method.GetGenericArguments();
            foreach (var argument in arguments)
            {
                AddReference(argument);
            }
        }

        private void AddPropertyTypes()
        {
            var resolver = new PropertyResolver(_context.CompositionType);
            var properties = resolver.Resolve();
            foreach (var property in properties)
            {
                AddPropertyTypes(property);
            }
        }

        private void AddPropertyTypes(PropertyInfo property)
        {
            AddReference(property.PropertyType);
        }

        private void AddEventTypes()
        {
            var resolver = new EventResolver(_context.CompositionType);
            var events = resolver.Resolve();
            foreach (var @event in events)
            {
                AddEventTypes(@event);
            }
        }

        private void AddEventTypes(EventInfo @event)
        {
            AddReference(@event.EventHandlerType);
        }

        private void AddReference(Type type)
        {
            if (type == null)
            {
                return;
            }

            AddInterfaces(type);
            AddAssembly(type.Assembly);
            AddReference(type.BaseType);
        }

        private void AddInterfaces(Type type)
        {
            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                AddReference(@interface);
            }
        }

        private void AddAssembly(Assembly assembly)
        {
            var location = assembly.Location;
            if (!_result.Contains(location))
            {
                _result.Add(location);
            }
        }
    }
}