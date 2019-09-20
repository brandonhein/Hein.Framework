using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Hein.Framework.Dynamo
{
    public abstract class DynamoRepositoryBase
    {
        private readonly RegionEndpoint _endpoint;
        public DynamoRepositoryBase(RegionEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        private static AmazonDynamoDBClient _client;
        protected AmazonDynamoDBClient GetDynamo()
        {
            if (_client == null)
            {
                var config = new AmazonDynamoDBConfig();

                config.RegionEndpoint = _endpoint;
                _client = new AmazonDynamoDBClient(config);
            }

            return _client;
        }

        protected DynamoDBContext GetContext()
        {
            return new DynamoDBContext(GetDynamo());
        }
    }
}
