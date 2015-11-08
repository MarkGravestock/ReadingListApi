using System.Collections.Generic;
using Raven.Client;
using ReadlingList.Domain;

namespace ReadingList.Persistance
{
    public class RavenDbReadingListQuery : IReadingListQuery
    {
        private readonly IDocumentStore documentStore;

        public RavenDbReadingListQuery(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public IEnumerable<ItemToRead> GetAllItemsToRead()
        {
            using (var documentSession = documentStore.OpenSession())
            {
                return documentSession.Query<ItemToRead>();
            }
        }

        public ItemToRead GetItemToRead(string id)
        {
            using (var documentSession = documentStore.OpenSession())
            {
                return documentSession.Load<ItemToRead>(id);
            }
        }
    }
}
