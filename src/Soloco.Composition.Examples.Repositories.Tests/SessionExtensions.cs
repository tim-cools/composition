using System;
using System.Collections.Generic;

using NHibernate;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public static class SessionExtensions
    {
        public static void PersistAll<T>(this ISession session, IEnumerable<T> items)
        {
            if (session == null) throw new ArgumentNullException("session");

            foreach (var item in items)
            {
                session.Persist(item);
            }
        }

        public static IList<T> GetAll<T>(this ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            return session.CreateCriteria(typeof(T))
                          .List<T>();
        }
    }
}