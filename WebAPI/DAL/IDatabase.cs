namespace WebAPI.DB
{
    public interface IDatabase
    {
        Task Execute(string sql);
        Task<List<KeyValuePair<int, int>>> GetDocuments(List<int> wordIds);
        string AsString(List<int> x);
        Task<Dictionary<string, int>> GetAllWords();
        Task<List<string>> GetDocDetails(List<int> docIds);
    }
}
