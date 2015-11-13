using Microsoft.AspNet.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Raven.Client;
using Raven.Client.Embedded;
using ReadlingList.Domain;
using Xunit;
using Ploeh.AutoFixture;
using Should.Fluent;

namespace ReadingList.Api.AcceptanceTests
{
    public class ReadingListJsonTests : IDisposable
    {
        public readonly string HostUri = "http://localhost:5123";
        public readonly string ReadingListPath = "api/ReadingList";
        public readonly string MediaTypeJson = "application/json";

        private readonly IHostingEngine host;
        private readonly HttpClient client;

        private readonly IDocumentStore documentStore;

        private readonly Fixture fixture;

        public ReadingListJsonTests()
        {
            documentStore = new EmbeddableDocumentStore {DefaultDatabase = "ReadingList", RunInMemory = true};

            host = new TestWebHostBuilder().UsingUri(HostUri).UsingDocumentStore(documentStore).Build();

            documentStore.Initialize();
            
            client = new HttpClient { BaseAddress = new Uri(HostUri) };

            fixture = new Fixture();
        }

        [Fact]
        public void ReturnsExpectedHttpResult()
        {
            using (host.Start())
            {
                var response = client.GetAsync(ReadingListPath).Result;

               response.IsSuccessStatusCode.Should().Be.True();
            }
        }

        [Fact]
        public void ReturnsExpectedJsonContent()
        {
            using (host.Start())
            {
                var response = client.GetAsync(ReadingListPath).Result;

                response.Content.Headers.ContentType.MediaType.Should().Equal(MediaTypeJson);
            }
        }

        [Fact]
        public void ReturnsTheExpectedItemToRead()
        {
            var itemToRead = CreateItemToRead(fixture.Create<String>());

            Save(itemToRead);

            using (host.Start())
            {
                var response = client.GetAsync(ReadingListPath + "/" + itemToRead.Id).Result;
                var itemsToRead = response.Content.ReadAsync<ItemToRead>().Result;

                itemsToRead.Should().Not.Be.Null();
            }
        }

        [Fact]
        public void ReturnsAtLeastOneItemToRead()
        {
            var itemToRead = CreateItemToRead(fixture.Create<String>());

            Save(itemToRead);

            var secondItemToRead = CreateItemToRead(fixture.Create<String>());

            Save(secondItemToRead);

            using (var app = host.Start())
            {
                var response = client.GetAsync(ReadingListPath).Result;
                var itemToReads = response.Content.ReadAsync<ItemToRead[]>().Result;

                itemToReads.Length.Should().Equal(2);
            }
        }

        private void Save(ItemToRead itemToRead)
        {
            using (var documentSession = documentStore.OpenSession())
            {
                documentSession.Store(itemToRead);
                documentSession.SaveChanges();
            }
        }

        private static ItemToRead CreateItemToRead(String id)
        {
            var itemToRead = new ItemToRead
            {
                Id = id,
                Description = "Solid Javascript",
                Uri = new Uri("https://www.youtube.com/watch?v=TAVn7s-kO9o"),
                Tags = new List<string>() {"Development", "SOLID"}
            };

            return itemToRead;
        }

        public void Dispose()
        {
            documentStore.Dispose();
        }
    }
}