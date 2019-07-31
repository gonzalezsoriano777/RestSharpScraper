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
    class Program
    {
        static void Main(string[] args)
        {
            apiScraper apiScrape = new apiScraper();
            apiScrape.ScrapeFromContent();

        }
    }
}
