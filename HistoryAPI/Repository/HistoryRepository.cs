using HistoryAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HistoryAPI.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HistoryDbContext _dbContext;

        public HistoryRepository(HistoryDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetHistory()
        {
            var list = await _dbContext.SearchHistories.ToListAsync();
            var resultList = new List<string>();

            foreach (var history in list) {
                resultList.Add(history.SearchHistory);
            }

            return resultList;
        }

        public async Task SaveHistory(string term)
        {
            var history = new History
            {
                SearchHistory = term
            };

            await _dbContext.SearchHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync();
        }
    }
}
