using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SHopMVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SHopMVC.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            CategoryListDto listDto;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44331/api/categories");

                var responseStr = await response.Content.ReadAsStringAsync();

                listDto = JsonConvert.DeserializeObject<CategoryListDto>(responseStr);
            }




            return View(listDto);
        }
    }
}
