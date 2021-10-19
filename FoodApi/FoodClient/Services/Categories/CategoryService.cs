using FoodClient.Models;
using FoodShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodClient.Services
{
    public class CategoryService
    {
        public CategoryService(HttpClient client)
        {
            _client = client;
        }

        public HttpClient _client { get; }

        //public async Task<bool> AddCategory(Category category)
        //{
        //    if (category == null) return false;

        //    var categoryJson = JsonSerializer.Serialize(category);
        //    var requestC = new StringContent(categoryJson, Encoding.UTF8, "application/Json");
        //    var response = await _client.PostAsync("/category/Add", requestC);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var createdCompany = JsonSerializer.Deserialize<bool>(content);

        //        return true;
        //    }

        //    return false;
        //}

        public List<Category> GetAll()
        {
            List<Category> categoys = null;
            var task = _client.GetAsync("Categories");
            task.Wait();
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsAsync<List<Category>>();
                read.Wait();
                categoys = read.Result;
                return categoys;

            }
            return categoys;
        }



    }
}
