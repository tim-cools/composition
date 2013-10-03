using System;
using System.Collections.Generic;

using FluentNHibernate;
using FluentNHibernate.Diagnostics;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public class TestTypeSource : ITypeSource
    {
        public IEnumerable<Type> GetTypes()
        {
            return new []
                       {
                           typeof (Employee), 
                           typeof (Store),
                           typeof (Product)
                       };
        }

        public void LogSource(IDiagnosticLogger logger)
        {
        }

        public string GetIdentifier()
        {
            return GetType().Name;
        }
    }
}