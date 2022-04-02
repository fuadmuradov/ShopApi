using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data.DAL;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext context;

        public CategoriesController(ShopDbContext context)
        {
            this.context = context;
        }

        public IActionResult GetAll()
        {
            return StatusCode(200, context.Categories.ToList());
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            Category category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            return StatusCode(200, category);
        }

        [HttpPost("")]
        public IActionResult Create(Category category)
        {
            Category existcategory = context.Categories.FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());
            if (existcategory != null) return Content($"{category.Name.ToUpper()} already exist!!");

            context.Categories.Add(category);
            context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPut("")]
        public IActionResult Update(Category category)
        {
            Category exisyCategory = context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (exisyCategory == null) return NotFound();
            exisyCategory.Name = category.Name;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            context.Categories.Remove(category);
            context.SaveChanges();
            return Ok();

        }



    }
}
