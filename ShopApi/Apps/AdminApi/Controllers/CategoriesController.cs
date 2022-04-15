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
using ShopApi.Repositories;
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
        private readonly IWebHostEnvironment webHost;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ShopDbContext context, IWebHostEnvironment webHost, IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.context = context;
            this.webHost = webHost;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var query = categoryRepository.GetAll(x => !x.IsDeleted);
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
        public async Task<IActionResult> Get(int id)
        {
            Category category = await categoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Products");
            if (category == null) return NotFound();
      
            CategoryGetDtos categoryGet = mapper.Map<CategoryGetDtos>(category);

            return StatusCode(200, categoryGet);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]CategoryPostDto categoryPostDto)
        {
            Category existcategory = await categoryRepository.GetAsync(x => x.Name.ToLower() == categoryPostDto.Name.ToLower());
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


            await categoryRepository.AddAsync(category);
            await categoryRepository.CommitAsync();

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryPostDto categoryPostDto)
        {
            Category exisyCategory = await categoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (exisyCategory == null) return NotFound();
            exisyCategory.Name = categoryPostDto.Name;
            await categoryRepository.CommitAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await categoryRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (category == null) return NotFound();

            categoryRepository.Remove(category);
            await categoryRepository.CommitAsync();
            return Ok();

        }



    }
}
