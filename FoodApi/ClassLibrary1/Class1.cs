using FoodApiClient;
using System;
using System.Net.Http;

namespace ClassLibrary1
{
    public class Class1
    {
        public Class1()
        {

        }

        public void get()
        {
            var httpClient = new HttpClient();
            var client = new swaggerClient("https://localhost:5001/",httpClient);

            client.
        }
    }
}
