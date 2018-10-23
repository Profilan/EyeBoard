using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;

namespace Profilan.SharedKernel
{
    public static class SessionFactory
    {
        private static ISessionFactory _factory;

        public static ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        private static void Init()
        {
            _factory = BuildSessionFactory();
        }

        private static ISessionFactory BuildSessionFactory()
        {
            Configuration configuration = new Configuration();

            configuration.EventListeners.PostCommitUpdateEventListeners =
                new IPostUpdateEventListener[] { new EventListener() };
            configuration.EventListeners.PostCommitInsertEventListeners =
                new IPostInsertEventListener[] { new EventListener() };
            configuration.EventListeners.PostCommitDeleteEventListeners =
                new IPostDeleteEventListener[] { new EventListener() };
            configuration.EventListeners.PostCollectionUpdateEventListeners =
                new IPostCollectionUpdateEventListener[] { new EventListener() };

            return configuration.BuildSessionFactory();
        }
    }
}
