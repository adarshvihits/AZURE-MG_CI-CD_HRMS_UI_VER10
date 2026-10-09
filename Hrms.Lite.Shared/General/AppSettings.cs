namespace Hrms.Lite.Shared.General
{
    public class AppSettings
    {
        public string ApiBaseUrl { get; set; }
        public string ApiBaseUrl2 { get; set; }
        public Storage Storage { get; set; }
        public AwsStorage AwsStorage { get; set; }
        public string URL_Editing { get; set; }
        public Sftp Sftp { get; set; }
    }
    public class Storage
    {
        public string ConnectionString { get; set; }
        public string BlobContainer { get; set; }
    }
    public class AwsStorage
    {
       public string AwsAccessKey { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string AwsSessionToken { get; set; }
        public string BucketName { get; set; }
        public string Region { get; set; }
    }
    public class Sftp
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
    }
}
