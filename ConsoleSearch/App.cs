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
            ShowMenu();
        }

        private void ShowMenu()
        {
            Console.WriteLine("Please select a Load-Balancing strategy:");
            Console.WriteLine("1. Round Robin Strategy");
            Console.WriteLine("2. Least Connected Strategy");

            int choice;
            while ((choice = GetSelection()) != 0)
            {
                if (choice == 1)
                {
                    restClient.Post(new RestRequest("api/Configuration/SetStrategy?selection=" + 1, Method.Post));
                    RunSearch();
                }
                if (choice == 2)
                {
                    restClient.Post(new RestRequest("api/Configuration/SetStrategy?selection=" + 2, Method.Post));
                    RunSearch();
                }
                else
                {
                    Console.WriteLine("Please type one of the numbers above.");
                }
            }
        }

        private void RunSearch()
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:9002/");
            
            while (true)
            {
                Console.WriteLine("Enter Search Terms - Q for Quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.ToLower().Equals("q"))
                {
                    ShowMenu();
                    break;
                }
            
                Task<string> task = client.GetStringAsync("api/LoadBalancer?terms=" + input);
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
