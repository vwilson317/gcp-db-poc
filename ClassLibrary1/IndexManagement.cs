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

        public IndexContent(string collection, string field1, string order1, string field2, string order2)
        {
            collectionId = collection;
            IndexField indexField1 = new IndexField();
            indexField1.fieldPath = field1;
            indexField1.mode = order1;
            IndexField indexField2 = new IndexField();
            indexField2.fieldPath = field2;
            indexField2.mode = order2;
            IndexField[] fieldsArray = new IndexField[2];
            fieldsArray[0] = indexField1;
            fieldsArray[1] = indexField2;
            fields = fieldsArray;
        }
    }

    public static class IndexManagement
    {
        private const string api_uri_base = "https://firestore.googleapis.com/v1beta1/projects/";
        private const string index_uri = "/databases/(default)/indexes";
        public static async Task<HttpResponseMessage> CreateIndex<T>(string project, string field1, string order1, string field2, string order2)
            where T : new()
        {
            var x = new T();
            IndexContent indexContent = new IndexContent(typeof(T).CollectionName(), field1, order1, field2, order2);
            string jsonRequest = JsonConvert.SerializeObject(indexContent);
            string uriString = $"{api_uri_base}{project}{index_uri}";
            UriBuilder uri = new UriBuilder(uriString);
            return await HttpClientProvider.Client.PostAsync(uri.Uri, new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json"));
        }

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