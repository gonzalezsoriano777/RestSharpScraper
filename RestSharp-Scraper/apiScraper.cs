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
            RestClient Client = new RestClient("https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/get-summary");
            var request = new RestRequest(Method.GET);

            // Added the Host and Key, to grab the content of the api stocks
            request.AddHeader("X-RapidAPI-Host", "apidojo-yahoo-finance-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "bfab8a0181mshf12c4afb207144ap126f55jsnecf3c75cf681");

            IRestResponse response = Client.Execute(request);
            var content = response.Content;

            // parse data as a JsonObject and make it more formated
            object dataAsJsonObject = JsonConvert.DeserializeObject<apiScrapeTable>(content.ToString());

            var stockDataJsonArray = JArray.Parse(content);

            

            //Console.WriteLine(dataAsJsonObject);

            string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=apiDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection db = new SqlConnection(connectionString))
            {



                db.Open();
                Console.WriteLine("Database has been opened");
                Console.WriteLine();

                foreach (JToken stock in stockDataJsonArray)
                {

                    SqlCommand apiTable = new SqlCommand(" INSERT INTO dbo.[apiScrapeTable] ( StockRecord, ExchangeTimezoneName, FullExchangeName, Symbol, MarketChange) VALUES ( @stockRecord, @exchangeTimezoneName, @fullExchangeName, @symbol, @regularMarketChange )", db);

                    apiTable.Parameters.AddWithValue("@stockRecord", DateTime.Now);
                    apiTable.Parameters.AddWithValue("@exchangeTimezoneName", stock["exchangeTimezoneName"]);
                    apiTable.Parameters.AddWithValue("@fullExchangeName", stock["marketSummaryResponse.result.fullExchangeName"]);
                    apiTable.Parameters.AddWithValue("@symbol", stock["symbol"]);
                    apiTable.Parameters.AddWithValue("@regularMarketChange", stock["regularMarketChange.fmt"]);

                    apiTable.ExecuteNonQuery();
                }

                db.Close();
                Console.WriteLine("Database has been inserted with API Data!");
                Console.WriteLine();

            }

        }
    }
}
