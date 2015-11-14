using System;
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
        private Mock<IDocumentStore> documentStore;
        private Mock<IDocumentSession> documentSession;
        private RavenDbReadingListQuery sut;

        public RavenDbReadingListQueryTest()
        {
            documentStore = new Mock<IDocumentStore>();
            documentSession = new Mock<IDocumentSession>();
            sut = new RavenDbReadingListQuery(documentStore.Object);
        }

        [Fact]
        public void CanQueryForItemsToRead()
        {
            var expected = new Fixture().CreateMany<ItemToRead>().ToList();

            var itemToReadQueryable = new Mock<IRavenQueryable<ItemToRead>>();

            documentStore.Setup(x => x.OpenSession()).Returns(documentSession.Object);
            documentSession.Setup(x => x.Query<ItemToRead>()).Returns(itemToReadQueryable.Object);
            itemToReadQueryable.Setup(x => x.GetEnumerator()).Returns(expected.GetEnumerator());

            var actual = sut.GetAllItemsToRead();

            actual.Should().Count.Exactly(expected.Count);
        }

        [Fact]
        public void AnItemCanBeReadById()
        {
            var expectedId = new Fixture().Create<String>();
            var expectedItemToRead = new Fixture().Create<ItemToRead>();

            documentStore.Setup(x => x.OpenSession()).Returns(documentSession.Object);
            documentSession.Setup(x => x.Load<ItemToRead>(expectedId)).Returns(expectedItemToRead);

            var actual = sut.GetItemToRead(expectedId);

            actual.Should().Equal(expectedItemToRead);
        }
    }
}
