using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace ConsoleSearch
{
    public class App
    {
        public void Run()
        {
            //SearchLogic mSearchLogic = new SearchLogic(new Database());
            Console.WriteLine("Console Search");

            using HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:9000/");

            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q")) break;

                Task<string> task = client.GetStringAsync("api/search/" + input);
                task.Wait();

                string searchResult = task.Result;
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(searchResult);

                foreach (var ignored in result.IgnoredTerms)
                {
                    Console.WriteLine(ignored + "was ignored");
                }

                foreach (var resultDocument in result.Documents)
                {
                    Console.WriteLine(resultDocument.Id + ": " + resultDocument.Path + " - number of terms found: " +
                                      resultDocument.NumberOfOccurrences);
                }

                Console.WriteLine("Found " + result.Documents.Count + " in " + result.ElapsedMilliseconds + " ms");
                /*
                var wordIds = new List<int>();
                var searchTerms = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in searchTerms)
                {
                    int id = mSearchLogic.GetIdOf(t);
                    if (id != -1)
                    {
                        wordIds.Add(id);
                    }
                    else
                    {
                        Console.WriteLine(t + " will be ignored");
                    }
                }

                DateTime start = DateTime.Now;

                var docIds = mSearchLogic.GetDocuments(wordIds);

                // get details for the first 10             
                var top10 = new List<int>();
                foreach (var p in docIds.GetRange(0, Math.Min(10, docIds.Count)))
                {
                    top10.Add(p.Key);
                }

                TimeSpan used = DateTime.Now - start;

                int idx = 0;
                foreach (var doc in mSearchLogic.GetDocumentDetails(top10))
                {
                    Console.WriteLine("" + (idx+1) + ": " + doc + " -- contains " + docIds[idx].Value + " search terms");
                    idx++;
                }
                Console.WriteLine("Documents: " + docIds.Count + ". Time: " + used.TotalMilliseconds);
                */
            }
        }
    }
}
