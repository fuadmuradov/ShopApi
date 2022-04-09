using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
        public bool IsDeleted { get; set; }

    }
}
