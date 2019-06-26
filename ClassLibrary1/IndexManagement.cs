using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassLibrary1
{
    public enum ModeValues
    {
        ASCENDING,
        DESCENDING
    }

    public class IndexField
    {
        public string fieldPath;
        public string mode;
    }

    public class IndexContent
    {
        public string collectionId;
        public IndexField[] fields;

        public IndexContent(string collection, params IndexField[] indexFields)
        {
            collectionId = collection;
            fields = indexFields;
        }
    }

    public static class IndexManagement
    {
        private const string api_uri_base = "https://firestore.googleapis.com/v1beta1/projects/";
        private const string index_uri = "/databases/(default)/indexes";

        public static async Task<HttpResponseMessage> CreateIndex<T>(string project, params IndexField[] indexFields)
            where T : new()
        {
            if (indexFields.Count() < 2)
            {
                throw new ArgumentException(
                    "All individual fields are indexed automagically. Must use a two or more indexFields to create a composite index.");
            }

            var x = new T();
            IndexContent indexContent = new IndexContent(x.GetType().CollectionName(), indexFields);
            string jsonRequest = JsonConvert.SerializeObject(indexContent);
            string uriString = $"{api_uri_base}{project}{index_uri}";
            UriBuilder uri = new UriBuilder(uriString);
            return await HttpClientProvider.Client.PostAsync(uri.Uri, new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json"));
        }
    }
}