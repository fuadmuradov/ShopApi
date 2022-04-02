using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Apps.AdminApi.DTOs.ProductDtos;
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
            Product product = context.Products.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (product == null) return NotFound();

            ProductGetDto productGet = new ProductGetDto()
            {
                Id = product.Id,
                Name = product.Name,
                SalePrice = product.SalePrice,
                CostPrice = product.CostPrice,
                CreatedAt = product.CreatedAt,
                ModifiedAt = product.ModifiedAt,
                DisplayStatus = product.DisplayStatus,
                CategoryId = product.CategoryId

            };

            return StatusCode(200, productGet);
        }

        [Route("{page}")]
        public IActionResult GetAll(int page=1, string search=null)
        {

            var query = context.Products.Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search));

            ProductListDto productListDto = new ProductListDto()
            {
                Items = query.Skip((page-1)*4).Take(4).Select(x => new ProductListItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SalePrice = x.SalePrice,
                    CostPrice = x.CostPrice,
                    DisplayStatus = x.DisplayStatus
                }).ToList(),
                TotalCount = query.Count()
            };


            return StatusCode(StatusCodes.Status200OK, productListDto);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create(ProductPostDto productDto)
        {
            Product product = new Product()
            {
                Name = productDto.Name,
                SalePrice = productDto.SalePrice,
                CostPrice = productDto.CostPrice,
                DisplayStatus = productDto.DisplayStatus,
                CategoryId = productDto.CategoryId
            };

            context.Products.Add(product);
            context.SaveChanges();
            return StatusCode(201, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductPostDto productDto)
        {
            Product existProduct = context.Products.FirstOrDefault(x => x.Id == id);
            if (existProduct == null) return NotFound();

            existProduct.Name = productDto.Name;
            existProduct.SalePrice = productDto.SalePrice;
            existProduct.CostPrice = productDto.CostPrice;
            existProduct.DisplayStatus = productDto.DisplayStatus;
            existProduct.CategoryId = productDto.CategoryId;
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
