using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SHopMVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SHopMVC.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            CategoryListDto listDto;

            string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZnVhZG11cmFkb3YiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjczZTNlNmM4LTAzNzgtNDZlMS04ODlmLTNkOWI0ZTA0Y2RlOSIsIkZ1bGxOYW1lIjoiRnVhZCBNdXJhZG92IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTWVtYmVyIiwiZXhwIjoxNjQ5OTQyMDA1LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDMzMS8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMzMS8ifQ.iWO0nJHJQ8B8kdK4Hri-Qrv3fCSr1Sb5B7tI23OuAsU";

            //using (HttpClient client = new HttpClient())
            //{

            //    var response = await client.GetAsync("https://localhost:44331/api/categories");

            //    var responseStr = await response.Content.ReadAsStringAsync();

            //    listDto = JsonConvert.DeserializeObject<CategoryListDto>(responseStr);
            //}

            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44331/api/categories"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseStr = await response.Content.ReadAsStringAsync();
                listDto = JsonConvert.DeserializeObject<CategoryListDto>(responseStr);
            }


            return View(listDto);
        }
    }
}
