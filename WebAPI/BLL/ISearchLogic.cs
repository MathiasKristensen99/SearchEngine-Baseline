namespace WebAPI.Logic
{
    public interface ISearchLogic
    {
        int GetIdOf(string word);
        Task<List<KeyValuePair<int, int>>> GetDocuments(List<int> wordIds);
        Task<List<string>> GetDocumentDetails(List<int> docIds);
    }
}
