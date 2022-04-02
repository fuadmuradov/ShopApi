using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.AdminApi.DTOs.ProductDtos
{
    public class ProductListDto
    {
       public List<ProductListItemDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
