using System.Threading.Tasks;

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
            var x = await collection.GetSnapshotAsync();
            var entity = x.ConvertTo<T>();
            return entity;
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
