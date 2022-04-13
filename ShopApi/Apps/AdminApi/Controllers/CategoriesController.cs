using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Apps.AdminApi.DTOs.CateforyDtos;
using ShopApi.Data.DAL;
using ShopApi.Data.Entities;
using ShopApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext context;
        private readonly IWebHostEnvironment webHost;
        private readonly IMapper mapper;

        public CategoriesController(ShopDbContext context, IWebHostEnvironment webHost, IMapper mapper)
        {
            this.context = context;
            this.webHost = webHost;
            this.mapper = mapper;
        }

        public IActionResult GetAll()
        {
            var query = context.Categories.Where(x => !x.IsDeleted);
            CategoryGetListDto categoryGetListDto = new CategoryGetListDto()
            {
                Categories = query.Select(x => new CategoryListItemDto()
                {
                    Name = x.Name,
                    Image = x.Image
                }).ToList(),
                TotalCount = query.Count()
            };


            return StatusCode(200, categoryGetListDto);
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            Category category = context.Categories.Include(x=>x.Products).FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();
            //CategoryGetDtos categoryGet = new CategoryGetDtos()
            //{
            //    Name = category.Name,
            //    Image = category.Image,
            //};

            CategoryGetDtos categoryGet = mapper.Map<CategoryGetDtos>(category);

            return StatusCode(200, categoryGet);
        }

        [HttpPost("")]
        public IActionResult Create([FromForm]CategoryPostDto categoryPostDto)
        {
            Category existcategory = context.Categories.FirstOrDefault(x => x.Name.ToLower() == categoryPostDto.Name.ToLower());
            if (existcategory != null) return Content($"{categoryPostDto.Name.ToUpper()} already exist!!");

            if (categoryPostDto.Photo == null) return NotFound();

            if (!categoryPostDto.Photo.IsImage())
            {
                return NotFound();
            }

            string folder = @"Image\";
            string filename = categoryPostDto.Photo.SaveAsync(webHost.WebRootPath, folder).Result;

            Category category = new Category()
            {
                Name = categoryPostDto.Name,
                Image = filename
            };

            context.Categories.Add(category);
            context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryPostDto categoryPostDto)
        {
            Category exisyCategory = context.Categories.FirstOrDefault(x => x.Id == id);
            if (exisyCategory == null) return NotFound();
            exisyCategory.Name = categoryPostDto.Name;
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
