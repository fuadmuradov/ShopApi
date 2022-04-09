using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.AdminApi.DTOs.CateforyDtos
{
    public class CategoryGetDtos
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int ProductsCount { get; set; }
    }
}
