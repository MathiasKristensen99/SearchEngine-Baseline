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
            var wordTask = _mDatabase.GetAllWords();
            wordTask.Wait();
            mWords = wordTask.Result;
        }

        public int GetIdOf(string word)
        {
            if (mWords.ContainsKey(word))
                return mWords[word];
            return -1;
        }

        public async Task<List<KeyValuePair<int, int>>> GetDocuments(List<int> wordIds)
        {
            return await _mDatabase.GetDocuments(wordIds);
        }

        public async Task<List<string>> GetDocumentDetails(List<int> docIds)
        {
            return await _mDatabase.GetDocDetails(docIds);
        }
    }
}
