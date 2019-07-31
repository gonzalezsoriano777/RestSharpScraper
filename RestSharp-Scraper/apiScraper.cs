using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharp_Scraper
{
    public class apiScraper
    {
        public void ScrapeFromContent()
        {
            // Initialized restSharper to grab the api url
            RestClient Client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-summary");
            var request = new RestRequest(Method.GET);

            // Added the Host and Key, to grab the content of the api stocks
            request.AddHeader("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "bfab8a0181mshf12c4afb207144ap126f55jsnecf3c75cf681");

            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            // parse data as a JsonObject and make it more formated
            object dataAsJsonObject = JsonConvert.DeserializeObject(content);

            Console.WriteLine(dataAsJsonObject);
            Console.ReadKey();
        }

        public
    }
}
