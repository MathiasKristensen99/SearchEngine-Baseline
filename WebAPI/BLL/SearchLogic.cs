using System;
using System.Collections.Generic;
using WebAPI.DB;
using WebAPI.Logic;

namespace Logic
{
    public class SearchLogic : ISearchLogic
    {
        IDatabase _mDatabase;

        Dictionary<string, int> mWords;

        public SearchLogic(IDatabase database)
        {
            _mDatabase = database;
            mWords = _mDatabase.GetAllWords();
        }

        public int GetIdOf(string word)
        {
            if (mWords.ContainsKey(word))
                return mWords[word];
            return -1;
        }

        public List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds)
        {
            return _mDatabase.GetDocuments(wordIds);
        }

        public List<string> GetDocumentDetails(List<int> docIds)
        {
            return _mDatabase.GetDocDetails(docIds);
        }
    }
}
