using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using ReadlingList.Domain;
using System.Linq;

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

        public IEnumerable<ItemToRead> GetItemsToReadByTag(string tag)
        {
            using (var documentSession = documentStore.OpenSession())
            {
                return documentSession.Query<ItemToReadByTags.Result, ItemToReadByTags>().Where(x => x.Tag == tag).OfType<ItemToRead>().ToList();
            }
        }

        public class ItemToReadByTags : AbstractIndexCreationTask<ItemToRead>
        {
            public class Result
            {
                public string Tag;
            }

            public ItemToReadByTags()
            {
                Map = items => from item in items
                               from tag in item.Tags
                               select new
                               {
                                   Tag = tag
                               };
            }
        }
    }
}
