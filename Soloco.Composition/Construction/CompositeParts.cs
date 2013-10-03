using System;
using System.Collections.Generic;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Holds the instances of the composite parts.
    /// </summary>
    public class CompositeParts
    {
        private readonly List<Type> _keys = new List<Type>();
        private readonly List<object> _instances = new List<object>();

        /// <summary>
        /// Gets the part instances.
        /// </summary>
        public object[] GetInstances()
        {
            return _instances.ToArray();
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> instance for the specified part type.
        /// </summary>
        public object this[Type type]
        {
            get
            {
                var index = _keys.IndexOf(type);
                return index == -1 ? GetByType(type) : _instances[index];
            }
        }

        private object GetByType(Type type)
        {
            foreach (var instance in _instances)
            {
                if(instance.GetType() == type)
                {
                    return instance;
                }
            }
            return null;
        }

        /// <summary>
        /// Add an instance for the specified part type.
        /// </summary>
        public void Add(Type key, object instance)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (instance == null) throw new ArgumentNullException("instance");

            _keys.Add(key);
            _instances.Add(instance);
        }
    }
}