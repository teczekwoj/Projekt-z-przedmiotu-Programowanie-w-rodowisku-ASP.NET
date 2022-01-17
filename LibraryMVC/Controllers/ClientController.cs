using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //IConfiguration
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LibraryApi.Models;
using System;

namespace LibraryMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient client;
        private readonly string LibraryApiPath;
        private readonly IConfiguration _configuration;


        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
            LibraryApiPath = _configuration["LibraryApiConfig:Url"];   //read from appsettings.json
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["LibraryApiConfig:ApiKey"]);   //use on any http calls      
        }


        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            List<LibraryItem> items = null;
            HttpResponseMessage response = await client.GetAsync(LibraryApiPath);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<LibraryItem>>();  //requires System.Net.Http.Formatting.Extension
            }
            return View(items);
        }



        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(LibraryApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                LibraryItem item = await response.Content.ReadAsAsync<LibraryItem>();
                return View(item);
            }
            return NotFound();
        }


        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  //item add deoc to slides
        public async Task<ActionResult> Create([Bind("Id,Title,Author,Edition,CheckedOutByWhom,Condition,Category,Description")] LibraryItem item)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(LibraryApiPath, item);
                //response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }



        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(LibraryApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                LibraryItem item = await response.Content.ReadAsAsync<LibraryItem>();
                return View(item);
            }
            return NotFound();
        }


        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Title,Author,Edition,CheckedOutByWhom,Condition,Category,Description")] LibraryItem item)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(LibraryApiPath + id, item);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }




        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync(LibraryApiPath + id);
            if (response.IsSuccessStatusCode)
            {
                LibraryItem item = await response.Content.ReadAsAsync<LibraryItem>();
                return View(item);
            }
            return NotFound();
        }


        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, int notUsed = 0)
        {
            HttpResponseMessage response = await client.DeleteAsync(LibraryApiPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}
