using FoodClient.Services;
using FoodClient.Services.LocalStorage;
using FoodShared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodClient.Controllers
{
    public class AuthController : Controller
    {

        public ApiService _service;
        public readonly ILocalStorageService _localStorage;

        public AuthController(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _service = new ApiService(_localStorage);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _service.RegisterUser(model);
            if (result)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var token = await _service.Login(model);
            if (token != null)
            {
                await _service.saveToStorage(token); //

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpGet]
        public ActionResult Denied()
        {
            return View();
        }
    }
}
