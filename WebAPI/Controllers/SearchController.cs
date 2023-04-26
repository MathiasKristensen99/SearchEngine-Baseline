using Common;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAPI.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchLogic _searchLogic;

        public SearchController(ISearchLogic searchLogic)
        {
            _searchLogic = searchLogic;
        }

        // GET: api/<SearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SearchController>/5
        [HttpGet("{input}")]
        public async Task<SearchResult> GetSearchResult(string input)
        {
            // NOT WORKING
            // using var activity = DiagnosticsConfig.ActivitySource.StartActivity();
            var wordIds = new List<int>();
            var searchTerms = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var result = new SearchResult();

            foreach (var word in searchTerms)
            {
                int id = _searchLogic.GetIdOf(word);
                if (id != -1)
                {
                    wordIds.Add(id);
                }
            }

            DateTime start = DateTime.Now;

            var docIds = await _searchLogic.GetDocuments(wordIds);

            var top10 = new List<int>();
            foreach (var p in docIds.GetRange(0, Math.Min(10, docIds.Count)))
            {
                top10.Add(p.Key);
            }

            int idx = 0;

            foreach (var p in await _searchLogic.GetDocumentDetails(top10))
            {
                result.Documents.Add(new Document { Id = idx + 1, NumberOfOccurrences = docIds[idx].Value, Path = p });
                idx++;
            }

            result.ElapsedMilliseconds = (DateTime.Now - start).TotalMilliseconds;

            result.HostName = Environment.MachineName;
            
            // Not printing anything
            // Console.WriteLine("Searched words: " + input);
            // Log.Logger.Debug("User has searched for {input}", input);

            return result;
        }

        // POST api/<SearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
