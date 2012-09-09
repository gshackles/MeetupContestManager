using Raven.Client;
using Raven.Client.Document;

namespace MeetupContestManager.Core.DataAccess
{
    public abstract class RepositoryBase
    {
        private readonly DocumentStore _documentStore;

        protected RepositoryBase(string connectionStringName)
        {
            _documentStore = new DocumentStore {ConnectionStringName = connectionStringName};
            _documentStore.Conventions.IdentityPartsSeparator = "-";
            _documentStore.Initialize();
        }

        protected IDocumentSession GetSession()
        {
            return _documentStore.OpenSession();
        }
    }
}