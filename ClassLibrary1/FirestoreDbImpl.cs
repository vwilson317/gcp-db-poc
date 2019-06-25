using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;

namespace ClassLibrary1
{
    //todo: move to appconfig

    public static class FirestoreDbImpl
    {
        private static FirestoreDb _db;

        public static FirestoreDb Db => _db ?? Create();

        private static FirestoreDb Create()
        {
            var creds = CredProvider.GetCreds().ToChannelCredentials();
            var channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), creds);

            var client = FirestoreClient.Create(channel);
            var db = FirestoreDb.Create(ConfigValues.project, client);

            return db;
        }
    }
}