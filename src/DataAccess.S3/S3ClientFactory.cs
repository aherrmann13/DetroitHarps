namespace DetroitHarps.DataAccess.S3
{
    using Amazon.S3;

    public static class S3ClientFactory
    {
        public static IAmazonS3 CreateClient(string accessKey, string accessToken, string serviceUrl)
        {
            return new AmazonS3Client(
                accessKey,
                accessToken,
                new AmazonS3Config { ServiceURL = serviceUrl });
        }
    }
}