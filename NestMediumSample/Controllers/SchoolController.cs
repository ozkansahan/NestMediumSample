using Microsoft.AspNetCore.Mvc;
using NestMediumSample.Models;
using NestMediumSample.Services;
using System.Collections.Generic;

namespace NestMediumSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly INestClientService _nestClientService;

        public SchoolController(INestClientService nestClientService)
        {
            _nestClientService = nestClientService;
        }

        [HttpGet]
        public IEnumerable<School> Get(Query input)
        {
            var likesValues = new List<KeyValuePair<string, string>>();
            var matchValues = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(input.Address))
            {
                likesValues.Add(new KeyValuePair<string, string>("address", input.Address));
            }

            if (!string.IsNullOrEmpty(input.Name))
            {
                likesValues.Add(new KeyValuePair<string, string>("name", input.Name));
            }

            if (input.Age > default(int))
            {
                matchValues.Add(new KeyValuePair<string, string>("age", input.Age.ToString()));
            }

            if (!string.IsNullOrEmpty(input.Class))
            {
                matchValues.Add(new KeyValuePair<string, string>("class", input.Class));
            }

            var result = _nestClientService.Search<School>("school", matchValues, likesValues, default(int), 50);
            return result;
        }

    }
}
