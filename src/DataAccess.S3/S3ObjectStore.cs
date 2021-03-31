namespace DetroitHarps.DataAccess.S3
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Amazon.S3;
    using Amazon.S3.Model;
    using DetroitHarps.Business;
    using Newtonsoft.Json;
    using Tools;

    public class S3ObjectStore<T, I> : IS3ObjectStore<T, I>
        where T : IHasId<I>
    {
        private readonly IAmazonS3 _s3Client;
        private readonly S3ObjectStoreSettings _settings;
        private readonly IKeyConverter<I> _converter;
        private readonly Encoding _fileEncoding = Encoding.UTF8;

        public S3ObjectStore(IAmazonS3 s3Client, S3ObjectStoreSettings settings, IKeyConverter<I> converter)
        {
            Guard.NotNull(s3Client, nameof(s3Client));
            Guard.NotNull(settings, nameof(settings));
            Guard.NotNull(converter, nameof(converter));

            _s3Client = s3Client;
            _settings = settings;
            _converter = converter;
        }

        public async Task Put(T item)
        {
            var byteArray = _fileEncoding.GetBytes(JsonConvert.SerializeObject(item));
            var putRequest = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = GetFullKey(item.Id),
                InputStream = new MemoryStream(byteArray)
            };
            try
            {
                await _s3Client.PutObjectAsync(putRequest);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Error Putting File", ex);
            }
        }

        public async Task<T> Get(I id)
        {
            var getRequest = new GetObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = GetFullKey(id),
            };

            GetObjectResponse result;

            try
            {
                result = await _s3Client.GetObjectAsync(getRequest);
            }
            catch (Exception ex)
            {
                if (ex is AmazonS3Exception amazonEx && amazonEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(T);
                }

                throw new DataAccessException("Error Getting File", ex);
            }

            using (var reader = new StreamReader(result.ResponseStream, _fileEncoding))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }

        public async Task Delete(I id)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = GetFullKey(id),
            };
            try
            {
                await _s3Client.DeleteObjectAsync(deleteRequest);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Error Deleting File", ex);
            }
        }

        public async Task<IList<I>> GetAllIds()
        {
            var listRequest = new ListObjectsV2Request
            {
                BucketName = _settings.BucketName,
                Prefix = $"{_settings.KeyPrefix}/",
                Delimiter = "/"
            };

            ListObjectsV2Response response;
            try
            {
                response = await _s3Client.ListObjectsV2Async(listRequest);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Error Listing Objects", ex);
            }

            return response.S3Objects
                .Select(x => x.Key)
                .Select(x => x.Split('/').Last())
                .Select(x => _converter.FromString(x))
                .ToList();
        }

        private string GetFullKey(I id) => $"{_settings.KeyPrefix}/{_converter.ToString(id)}";
    }
}
