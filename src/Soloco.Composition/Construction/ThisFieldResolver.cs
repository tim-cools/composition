
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Soloco.Composition.Construction
{
    internal class ThisFieldResolver
    {
        private readonly object _cacheLock = new object();
        private readonly Dictionary<Type, IList<ThisField>> _cache = new Dictionary<Type, IList<ThisField>>();

        public IList<ThisField> Resolve(Type valueType)
        {
            if (valueType == null) throw new ArgumentNullException("valueType");
            
            lock (_cacheLock)
            {
                var fields = ResolveFromCache(valueType);
                if (fields == null)
                {
                    fields = ResolveWithReflection(valueType);
                    AddToCache(valueType, fields);
                }
                return fields;
            }
        }

        private void AddToCache(Type valueType, IList<ThisField> fields)
        {
            _cache.Add(valueType, fields);
        }

        private static IList<ThisField> ResolveWithReflection(Type valueType)
        {
            return valueType
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(f => f.GetCustomAttributes(typeof (ThisAttribute), true).Length > 0)
                .Select(f => new ThisField(f))
                .ToList();
        }

        private IList<ThisField> ResolveFromCache(Type valueType)
        {
            return _cache.ContainsKey(valueType) 
                ? _cache[valueType] 
                : null;
        }
    }
}