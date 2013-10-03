using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Soloco.Composition.Generation
{
    internal class CompositionPartInterfacesResolver
    {
        [DebuggerDisplay("{Interface} Usages: {Usages.Count}")]
        private class CompositionPartInterface
        {
            public Type InterfaceType { get; private set; }
            public List<Type> Usages { get; private set; }

            public CompositionPartInterface(Type @interface)
            {
                Usages = new List<Type>();
                InterfaceType = @interface;
            }
        }

        private Type _compositionType;
        private Dictionary<Type, CompositionPartInterface> _parts;

        public IList<Type> Resolve(Type compositionType)
        {
            if (compositionType == null) throw new ArgumentNullException("compositionType");

            _compositionType = compositionType;
            _parts = new Dictionary<Type, CompositionPartInterface>();

            return ResolveInterfaces();
        }

        private IList<Type> ResolveInterfaces()
        {
            AddInterfaces(_compositionType);
            GetUsages();
            VerifyInheritanceHirachy();

            return _parts
                .Where(k => k.Value.Usages.Count == 0)
                .Select(p => p.Key)
                .ToList();
        }

        private void VerifyInheritanceHirachy()
        {
            VerifyNoSharedBaseClasses();
        }

        private void VerifyNoSharedBaseClasses()
        {
            foreach (var @interface in _parts.Values)
            {
                VerifyNoSharedBaseClass(@interface);
            }
        }

        private static void VerifyNoSharedBaseClass(CompositionPartInterface @interface)
        {
            if (@interface.Usages.Count <= 1)
            {
                return;
            }
            var usagesAsString = FormatUsagesAsString(@interface);
            throw new InvalidCompositionException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "Composition base type '{0}' is shared by multiple composition parts: '{1}'.",
                    @interface.InterfaceType.FullName,
                    usagesAsString));
        }

        private static string FormatUsagesAsString(CompositionPartInterface @interface)
        {
            var usageBuilder = new StringBuilder();
            foreach (var usage in @interface.Usages)
            {
                if (usageBuilder.Length != 0)
                {
                    usageBuilder.Append(", ");
                }
                usageBuilder.Append(usage.FullName);
            }
            return usageBuilder.ToString();
        }

        private void AddInterfaces(Type interfaceType)
        {
            var types = interfaceType.GetInterfaces();
            foreach (var type in types)
            {
                AddInterface(type);
            }
        }

        private void AddInterface(Type interfaceType)
        {
            if (!_parts.ContainsKey(interfaceType))
            {
                _parts.Add(interfaceType, new CompositionPartInterface(interfaceType));
            }
            AddInterfaces(interfaceType);
        }

        private void GetUsages()
        {
            foreach (var type in _parts.Values)
            {
                GetUsages(type);
            }
        }

        private void GetUsages(CompositionPartInterface @interface)
        {
            foreach (var type in _parts.Values)
            {
                if (@interface != type)
                {
                    CheckUsage(@interface, type);
                }
            }
        }

        private static void CheckUsage(CompositionPartInterface @interface, CompositionPartInterface type)
        {
            var interfaces = type.InterfaceType.GetInterfaces();
            if (interfaces.Contains(@interface.InterfaceType))
            {
                @interface.Usages.Add(type.InterfaceType);
            }
        }
    }
}