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
            var request = new RestRequest(Method.GET);

            request.AddHeader("x-rapidapi-host", "morning-star.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "bfab8a0181mshf12c4afb207144ap126f55jsnecf3c75cf681");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            // Console.WriteLine(content);

            var stockDataAsJObject = JObject.Parse(content);
            Console.WriteLine(stockDataAsJObject);

            string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=apiDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                Console.WriteLine("Database has been opened!");
                Console.WriteLine();

                foreach(var stock in stockDataAsJObject)
                {
                    SqlCommand insertion = new SqlCommand("INSERT INTO dbo.apiScrapeTable (StockRecord, Symbol, LastPrice, PercentChange, MarketChange) VALUES(@stockRecord, @symbol, @lastPrice, @percent_change, @marketChange ) ", db);

                    insertion.Parameters.AddWithValue("@stockRecord", DateTime.Now);
                    insertion.Parameters.AddWithValue("@symbol", );
                    insertion.Parameters.AddWithValue("@lastPrice", );
                    insertion.Parameters.AddWithValue("@percent_change", );
                    insertion.Parameters.AddWithValue("@marketChange", );

                    insertion.ExecuteNonQuery();

                }

                db.Close();
                Console.WriteLine("Database has been updated with data!");
                Console.WriteLine();


            }

        }
    }
}
