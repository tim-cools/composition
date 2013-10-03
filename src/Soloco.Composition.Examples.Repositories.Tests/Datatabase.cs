using System.Collections.Generic;

using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Testing;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public static class Datatabase
    {
        public static ISessionSource CreateSessionSource()
        {
            var config = CreateConfiguration();
            var model = CreatePersistenceModel();

            var source = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(config, model);
            source.BuildSchema(true);
            return source;
        }

        private static IDictionary<string, string> CreateConfiguration()
        {
            return SQLiteConfiguration.Standard
                .InMemory()
                .ShowSql()
                .ToProperties();
        }

        private static AutoPersistenceModel CreatePersistenceModel()
        {
            var model = new AutoPersistenceModel();
            model.AddTypeSource(new TestTypeSource());
            model.Conventions.Add(DefaultLazy.Never());
            return model;
        }
    }
}