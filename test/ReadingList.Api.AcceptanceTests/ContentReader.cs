using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ReadingList.Api.AcceptanceTests
{
    public static class ContentReader
    {
        public static Task<T> ReadAsync<T>(this HttpContent content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            return content.ReadAsStringAsync().ContinueWith(t =>
                JsonConvert.DeserializeObject<T>(t.Result));
        }

        public static Task<XDocument> ReadAsXmlAsync(this HttpContent content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            return content.ReadAsStringAsync().ContinueWith(t =>
                XDocument.Parse(t.Result));
        }

        public static dynamic ToJObject(this object value)
        {
            return JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(value));
        }
    }
}
