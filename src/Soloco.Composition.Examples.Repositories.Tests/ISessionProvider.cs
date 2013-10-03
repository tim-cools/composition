using NHibernate;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public interface ISessionProvider
    {
        ISession Get();
    }
}