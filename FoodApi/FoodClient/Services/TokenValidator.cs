using FoodClient.Services.LocalStorage;
using FoodShared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodClient.Services
{
    public class TokenValidator
    {
        public readonly ILocalStorageService _localStorage;


        public TokenValidator(ILocalStorageService localStorage)
        {
            this._localStorage = localStorage;

        }

        public async Task CheckTokenValidity()
        {
            var expirationTime = DateTime.Parse(await _localStorage.GetItem<string>("tokenExpirationTime"));
            var currentTime = DateTime.Now;

            if (expirationTime < currentTime)
            {
                redirectToLogin();
            }

        }

        public static RedirectResult redirectToLogin()
        {
            return new RedirectResult(AppSettings.ApiUrl + "api/Account/Login");
        }
    }

}
