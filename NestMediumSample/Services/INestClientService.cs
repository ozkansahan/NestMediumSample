using System.Collections.Generic;

namespace NestMediumSample.Services
{
    public interface INestClientService
    {
        List<T> Search<T>(string indexName,
            List<KeyValuePair<string, string>> matchValues,
            List<KeyValuePair<string, string>> likeValues,
            int skip,
            int take) where T : class;
    }
}
