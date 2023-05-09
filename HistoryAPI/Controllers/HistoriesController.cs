using HistoryAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoriesController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetHistory()
        {
            var history = await _historyRepository.GetHistory();
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveHistory(string Term)
        {
            await _historyRepository.SaveHistory(Term);
            return Ok();
        }
    }
}
