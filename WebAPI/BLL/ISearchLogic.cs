namespace WebAPI.Logic
{
    public interface ISearchLogic
    {
        int GetIdOf(string word);
        List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds);
        List<string> GetDocumentDetails(List<int> docIds);
    }
}
