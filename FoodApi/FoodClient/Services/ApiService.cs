using FoodClient.Services.LocalStorage;
using FoodShared;
using FoodShared.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FoodClient.Services
{
    public class ApiService
    {
        public readonly ILocalStorageService _localStorage;
        public TokenValidator TokenValidator;
        public HttpClient http;
        public ApiService(ILocalStorageService localStorage)
        {

            _localStorage = localStorage;

        }

        public async Task<bool> RegisterUser(Register register)
        {

            var http = new HttpClient();

            var json = JsonConvert.SerializeObject(register);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.PostAsync(AppSettings.ApiUrl + "/Accounts/Register", content);

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public async Task<Token> Login(Login login)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/Accounts/Login", content);
            if (!response.IsSuccessStatusCode) return null;
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(jsonResult);


        }

        public async Task saveToStorage(Token result)
        {
            await _localStorage.SetItem("accessToken", result.access_token);
            await _localStorage.SetItem("userId", result.user_Id);
            await _localStorage.SetItem("userName", result.user_name);
            await _localStorage.SetItem("tokenExpirationTime", result.expiration_Time);
            await _localStorage.SetItem("currentTime", DateTime.Now);
        }

        public async Task<List<Category>> GetCategories()
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/Categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public async Task<Product> GetProductById(int productId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/Products/" + productId);
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public async Task<List<ProductByCategory>> GetProductByCategory(int categoryId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/Products/ProductsByCategory/" + categoryId);
            return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
        }

        public async Task<List<PopularProduct>> GetPopularProducts()
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/Products/PopularProducts");
            return JsonConvert.DeserializeObject<List<PopularProduct>>(response);
        }

        public async Task<bool> AddItemsInCart(AddToCart addToCart)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(addToCart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/ShoppingCartItems", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public async Task<CartSubTotal> GetCartSubTotal(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/ShoppingCartItems/SubTotal/" + userId);
            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/ShoppingCartItems/" + userId);
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public async Task<TotalCartItem> GetTotalCartItems(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/ShoppingCartItems/TotalItems/" + userId);
            return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public async Task<bool> ClearShoppingCart(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "/ShoppingCartItems/" + userId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public async Task<OrderResponse> PlaceOrder(Order order)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/Orders", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public async Task<List<OrderByUser>> GetOrdersByUser(int userId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/Orders/OrdersByUser/" + userId);
            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public async Task<List<Order>> GetOrderDetails(int orderId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await _localStorage.GetItem<string>("accessToken"));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/Orders/OrderDetails/" + orderId);
            return JsonConvert.DeserializeObject<List<Order>>(response);
        }

    }


}
