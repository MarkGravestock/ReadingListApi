using Microsoft.AspNet.Hosting.Internal;
using System;
using System.Net.Http;
using Xunit;

namespace ReadingListApi.AcceptanceTests
{
    public class ReadingListJsonTests
    {
        public readonly string HostUri = "http://localhost:5123";
        public readonly string ReadingListPath = "api/ReadingList";
        public readonly string MediaTypeJson = "application/json";

        public readonly IHostingEngine host;
        public readonly HttpClient client;

        public ReadingListJsonTests()
        {
            host = new TestWebHostBuilder().UsingUri(HostUri).Build();
            client = new HttpClient { BaseAddress = new Uri(HostUri) };
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
    }
}