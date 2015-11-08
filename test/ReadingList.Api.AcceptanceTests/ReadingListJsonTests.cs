using Microsoft.AspNet.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Ploeh.AutoFixture;
using Raven.Client;
using Raven.Client.Document;
using ReadlingList.Domain;
using Xunit;

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
            //documentStore = new EmbeddableDocumentStore {DefaultDatabase = "ReadingList"};
            documentStore = new DocumentStore { Url = "http://localhost:8080", DefaultDatabase = "ReadingList" };

            host = new TestWebHostBuilder().UsingUri(HostUri).UsingDocumentStore(documentStore).Build();

            documentStore.Initialize();
            
            client = new HttpClient { BaseAddress = new Uri(HostUri) };

            fixture = new Fixture();
        }

        [Fact]
        public void ReturnsExpectedHttpResult()
        {
            using (var app = host.Start())
            {
                var response = client.GetAsync(ReadingListPath).Result;

                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }
        }

        [Fact]
        public void ReturnsExpectedJsonContent()
        {
            using (var app = host.Start())
            {
                var response = client.GetAsync(ReadingListPath).Result;

                Assert.Equal(MediaTypeJson, response.Content.Headers.ContentType.MediaType);
            }
        }

        [Fact]
        public void ReturnsAtLeastOneItemToRead()
        {
            var itemToRead = new ItemToRead
            {
                Id = "47",
                Description = "Solid Javascript",
                Uri = new Uri("https://www.youtube.com/watch?v=TAVn7s-kO9o"),
                Tags = new List<string>() { "Development", "SOLID" }
            };

            using (var documentSession = documentStore.OpenSession())
            {
                documentSession.Store(itemToRead);
                documentSession.SaveChanges();
            }

            using (var app = host.Start())
            {
                var response = client.GetAsync(ReadingListPath + "/" + itemToRead.Id).Result;
                var json = response.Content.ReadAsync<ItemToRead>().Result;

                Assert.Equal(itemToRead.Description, json.Description);
            }
        }

        public void Dispose()
        {
            documentStore.Dispose();
        }
    }
}