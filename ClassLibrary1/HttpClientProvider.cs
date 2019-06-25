using System.Net.Http;

namespace ClassLibrary1
{
    public static class HttpClientProvider
    {
        public static HttpClient Client { get; }
        static HttpClientProvider()
        {
            var credential = CredProvider.GetCreds();
            // Inject the Cloud Platform scope if required.
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    "https://www.googleapis.com/auth/cloud-platform"
                });
            }
            Client = new Google.Apis.Http.HttpClientFactory()
                .CreateHttpClient(
                    new Google.Apis.Http.CreateHttpClientArgs()
                    {
                        ApplicationName = "Google Cloud Platform Firestore Sample",
                        GZipEnabled = true,
                        Initializers = { credential },
                    });
        }
    }
}