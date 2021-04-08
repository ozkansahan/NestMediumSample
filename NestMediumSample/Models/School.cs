using System.Collections.Generic;
using System.Linq;
using Nest;

namespace NestMediumSample.Models
{
    [ElasticsearchType(RelationName = "school")]
    public class School
    {
        [PropertyName("name")]
        public string Name { get; set; }

        [PropertyName("class")]
        public string Class { get; set; }

        [PropertyName("address")]
        public string Address { get; set; }

        [PropertyName("age")]
        public int Age { get; set; }

        public SearchDescriptor<School> GetQuery(List<KeyValuePair<string, string>> matchValues, List<KeyValuePair<string, string>> likeValues)
        {
            var mustContainer = new List<QueryContainer>();

            if (matchValues.Count > 0)
            {
                mustContainer.AddRange(matchValues.Select(item => new MatchQuery() { Field = item.Key, Query = item.Value }).Select(qc => (QueryContainer)qc).ToArray());
            }
            if (likeValues.Count > 0)
            {
                mustContainer.AddRange(likeValues.Select(item => new QueryStringQuery() { Fields = item.Key, Query = $"*{item.Value}*" }).Select(qc => (QueryContainer)qc).ToArray());
            }

            return new SearchDescriptor<School>().Query(x => x.Bool(b => b.Must(mustContainer.ToArray())));
        }


    }
}
