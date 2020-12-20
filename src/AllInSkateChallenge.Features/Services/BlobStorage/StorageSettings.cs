namespace AllInSkateChallenge.Features.Services.BlobStorage
{
    public class StorageSettings
    {
        public string ConnectionString { get; set; }

        public string ProfileContainer => "skaterprofiles";
    }
}