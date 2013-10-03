using FluentNHibernate;

using NHibernate;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    /// <summary>
    /// Ensures that we have only one session per test (unity container).
    /// We need to use this class because it is not possible to manage
    /// the livetime of a dependencies (ISession) with a static factory
    /// method.
    /// </summary>
    public class SessionProvider : ISessionProvider
    {
        private static readonly ISessionSource _source;
        private ISession _session;

        static SessionProvider()
        {
            //We only need to create it once
            _source = Datatabase.CreateSessionSource();
        }

        public ISession Get()
        {
            return _session ?? (_session = _source.CreateSession());
        }
    }
}