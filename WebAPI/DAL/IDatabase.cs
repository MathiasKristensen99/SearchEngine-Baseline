namespace WebAPI.DB
{
    public interface IDatabase
    {
        void Execute(string sql);
        List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds);
        string AsString(List<int> x);
        Dictionary<string, int> GetAllWords();
        List<string> GetDocDetails(List<int> docIds);
    }
}
