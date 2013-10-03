using NHibernate;

namespace Soloco.Composition.Example.Repositories.Services
{
    class Repository : IRepository
    {
        /// <summary>
        /// Gets or sets the current nHibernate session.
        /// </summary>
        /// <value>The session.</value>
        public ISession Session { get; private set; }

        public Repository(ISession session)
        {
            Session = session;
        }
    }
}