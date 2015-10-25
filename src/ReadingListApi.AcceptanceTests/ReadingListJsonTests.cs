using System;
using System.Net.Http;
using Xunit;

namespace ReadingListApi.AcceptanceTests
{
    public class ReadingListJsonTests
    {
        [Fact]
        public void ReturnsExpectedHttpResult()
        {
            var uri = "http://localhost:5123";

            var host = new TestWebHostBuilder().UsingUri(uri).Build();

            using (var app = host.Start())
            {
                var client = new HttpClient {BaseAddress = new Uri(uri)};

                var response = client.GetAsync("api/ReadingList").Result;

                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }
        }
    }
}