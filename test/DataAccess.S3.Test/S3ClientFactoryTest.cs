namespace DetroitHarps.DataAccess.S3.Test
{
    using Amazon.S3;
    using DetroitHarps.DataAccess.S3;
    using Xunit;

    public class S3ClientFactoryTest
    {
        [Fact]
        public static void CreateClientCreatesAmazonS3Client()
        {
            // TODO: how do i verify the correct params were passed in?
            var client = S3ClientFactory.CreateClient("key", "secret", "url");

            Assert.IsType<AmazonS3Client>(client);
        }
    }
}