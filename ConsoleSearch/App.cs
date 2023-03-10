using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common;
using RestSharp;
using Newtonsoft.Json;

namespace ConsoleSearch
{
    public class App
    {
        private RestClient restClient = new RestClient("http://localhost:9002/");
        public void Run()
        {
            //SearchLogic mSearchLogic = new SearchLogic(new Database());
            Console.WriteLine("Console Search");
            Console.WriteLine();

            ShowMenu();
        }

        private void ShowMenu()
        {
            int choice;
            while ((choice = GetSelection()) != 0)
            {
                if (choice == 1)
                {
                    restClient.Post(new RestRequest("api/Configuration/SetStrategy?selection=" + choice, Method.Post));
                    
                    RunSearch();
                }
                if (choice == 2)
                {
                    RunSearch();
                }
            }
        }

        private void RunSearch()
        {
            using HttpClient _client = new();
            _client.BaseAddress = new Uri("http://localhost:9002/");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q")) break;
            
                Task<string> task = _client.GetStringAsync("api/LoadBalancer?terms=" + input);
                task.Wait();

                string searchResult = task.Result;
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(searchResult);

                Console.WriteLine(result.HostName);

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
            }
        }
        
        private int GetSelection()
        {
            var selection = Console.ReadLine();
            if (int.TryParse(selection, out var selectionInt))
            {
                return selectionInt;
            }

            return -1;
        }
    }
}
