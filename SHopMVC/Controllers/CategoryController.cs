using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SHopMVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SHopMVC.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            CategoryListDto listDto;

            string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibXVyYWRtdXJhZG92IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJmZDEyZmQ5Mi0wYWYzLTQwNWMtYjhhMS0yNWVkNzg2Nzg4ZjkiLCJGdWxsTmFtZSI6Ik11cmFkIE11cmFkb3YiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNZW1iZXIiLCJleHAiOjE2NTA0MDQ4NjIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzMxLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzMxLyJ9.27XUF0ZJN7EE5x8mlL4Ah6kbS3a6OS6RyYjHmGFaKnI";

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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDto categoryPostDto)
        {

                CategoryPostDto receviedPostDto = new CategoryPostDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(categoryPostDto), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44331/api/categories/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        receviedPostDto = JsonConvert.DeserializeObject<CategoryPostDto>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index), "Category");
            

            
        }
    }
}
