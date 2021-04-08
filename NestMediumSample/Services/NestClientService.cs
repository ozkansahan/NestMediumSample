using System;
using System.Collections.Generic;
using Nest;

namespace NestMediumSample.Services
{
    public class NestClientService : INestClientService
    {
        private IElasticClient elasticSearchClient { get; }
        public NestClientService()
        {

            elasticSearchClient = GetClient();
        }

        private ElasticClient GetClient()
        {
            var connectionString = new Uri("ElasticSearch_Application_End_Point_URL");
            var connectionSettings = new ConnectionSettings(connectionString);
            connectionSettings.DefaultFieldNameInferrer(p => p);

            return new ElasticClient(connectionSettings);
        }


        public virtual List<T> Search<T>(string indexName,
            List<KeyValuePair<string, string>> matchValues,
            List<KeyValuePair<string, string>> likeValues,
            int skip,
            int take) where T : class
        {
            var method = typeof(T).GetMethod("GetQuery");
            return (List<T>) elasticSearchClient.Search<T>(
                ((SearchDescriptor<T>) method.Invoke(Activator.CreateInstance<T>(), new object[] {matchValues, likeValues}))
                .Index(indexName).From(skip).Size(take))?.Documents;
        }

    }
}