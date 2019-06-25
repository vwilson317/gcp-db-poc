using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace ClassLibrary1
{
    public static class DataAccess
    {
        public static async Task<string> Add<T>(T obj)
        {
            var collection = FirestoreDbImpl.Db.Collection(FirestoreCollection.CollectionName);

            var docRef = await collection.AddAsync(obj);
            return docRef.Id;
        }

        public static async Task<T> Get<T>(string id)
        {
            var collection = FirestoreDbImpl.Db.Collection(typeof(T).CollectionName()).Document(id);
            var snapshot = await collection.GetSnapshotAsync();
            var entity = snapshot.ConvertTo<T>();
            return entity;
        }

        public static async Task<IEnumerable<T>> Get<T>(params (string fieldName, dynamic value)[] queries)
        {
            var collection = FirestoreDbImpl.Db.Collection(typeof(T).CollectionName());
            Query query = null;

            foreach (var currentQuery in queries)
            {
                if (query == null)
                {
                    query = collection.WhereEqualTo(currentQuery.fieldName, currentQuery.value);
                }
            }

            var snapshot = await query.GetSnapshotAsync();
            var entities = snapshot.Documents.Select(d => d.ConvertTo<T>());
            return entities;
        }

        public static async Task Delete<T>(string id)
        {
            var collection = FirestoreDbImpl.Db.Collection(typeof(T).CollectionName()).Document(id);
            await collection.DeleteAsync();
        }

        public static async Task Update<T>(string id, T obj)
        {
            var collection = FirestoreDbImpl.Db.Collection(typeof(T).CollectionName()).Document(id);
            await collection.SetAsync(obj);
        }
    }
}
