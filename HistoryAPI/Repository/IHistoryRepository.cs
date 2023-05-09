namespace HistoryAPI.Repository
{
    public interface IHistoryRepository
    {
        Task<List<String>> GetHistory();
        Task SaveHistory(String term);
    }
}
