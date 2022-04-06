using FluentValidation;
using Microsoft.AspNetCore.Http;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.AdminApi.DTOs.ProductDtos
{
    public class ProductPostDto
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool DisplayStatus { get; set; }
        public int CategoryId { get; set; }
        
    }

    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("uzunluq 30 ola bilerr !")
                .NotEmpty().WithMessage("Name mütleq doldurulmalidir");

            RuleFor(x => x.CostPrice).GreaterThanOrEqualTo(5).WithMessage("CostPrice 5 den boyuk olmalidir")
                .NotNull().WithMessage("CostPrice Mecburidir!");

            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(5).WithMessage("SalePrice 5 den boyuk olmalidir")
               .NotNull().WithMessage("SalePrice Mecburidir!");

            RuleFor(x => x).Custom((x, context) =>
            {

                if (x.CostPrice > x.SalePrice)
                    context.AddFailure("CostPrice", "Costprice Saleprice dan boyuk ola bilmez");

            });
        }
    }
}
