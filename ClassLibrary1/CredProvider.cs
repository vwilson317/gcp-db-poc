using Google.Apis.Auth.OAuth2;

namespace ClassLibrary1
{
    public static class CredProvider
    {

        public static GoogleCredential GetCreds()
        {
            var googleCreds = GoogleCredential.FromFile($"{ConfigValues.cred_file}");
            return googleCreds;
        }
    }
}