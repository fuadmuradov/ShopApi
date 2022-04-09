using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.AdminApi.DTOs.CateforyDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }

        public IFormFile Photo { get; set; }
    }

    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Category Name not be null");
        }


    }

}
