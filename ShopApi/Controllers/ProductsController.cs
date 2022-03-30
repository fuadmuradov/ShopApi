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
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext context;

        public ProductsController(ShopDbContext db)
        {
            context = db;
        }

        [Route("get/{id}")]
        public IActionResult Get(int id)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            return StatusCode(200, product);
        }

        [Route("")]
        public IActionResult GetAll()
        {
            return StatusCode(StatusCodes.Status200OK, context.Products.Where(x=>x.DisplayStatus==true).ToList());
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return StatusCode(201, product);
        }

        [HttpPut("")]
        public IActionResult Update(Product product)
        {
            Product existProduct = context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existProduct == null) return NotFound();

            existProduct.Name = product.Name;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CostPrice = product.CostPrice;
            context.SaveChanges();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            context.Products.Remove(product);
            context.SaveChanges();
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult DisplayStatus(int id, bool status)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();


            product.DisplayStatus = status;
            context.SaveChanges();
            return NoContent();
        }


    }
}
