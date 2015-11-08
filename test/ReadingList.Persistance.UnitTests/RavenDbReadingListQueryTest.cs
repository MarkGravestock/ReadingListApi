using System.Linq;
using Moq;
using Ploeh.AutoFixture;
using Raven.Client;
using Raven.Client.Linq;
using ReadlingList.Domain;
using Should.Fluent;
using Xunit;

namespace ReadingList.Persistance.UnitTests
{
    public class RavenDbReadingListQueryTest
    {
        [Fact]
        public void CanQueryForItemsToRead()
        {
            var documentStore = new Mock<IDocumentStore>();
            var documentSession = new Mock<IDocumentSession>();
            var itemToRead = new Mock<IRavenQueryable<ItemToRead>>();
            var sut = new RavenDbReadingListQuery(documentStore.Object);
            var expected = new Fixture().CreateMany<ItemToRead>().ToList();

            documentStore.Setup(x => x.OpenSession()).Returns(documentSession.Object);
            documentSession.Setup(x => x.Query<ItemToRead>()).Returns(itemToRead.Object);
            itemToRead.Setup(x => x.GetEnumerator()).Returns(expected.GetEnumerator());

            var actual = sut.GetAllItemsToRead();

            actual.Should().Count.Exactly(expected.Count);
        }
    }
}
