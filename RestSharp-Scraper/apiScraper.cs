using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using RestSharp_Scraper.apiModel;

namespace RestSharp_Scraper
{
    public class ApiScraper
    {
        public void ScrapeFromContent()
        {
            // Initialized restSharper to grab the api url
            RestClient client = new RestClient("https://morning-star.p.rapidapi.com/market/get-summary");
            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("x-rapidapi-host", "morning-star.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "bfab8a0181mshf12c4afb207144ap126f55jsnecf3c75cf681");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            // Console.WriteLine(content);

            dynamic stockDataAsJObject = JsonConvert.DeserializeObject(content);

             Console.WriteLine(stockDataAsJObject["MarketRegions"]["USA"][0]["Exchange"]);

            //Console.WriteLine(stockDataAsJObject["MarketRegions"]);


        }
    }
}
