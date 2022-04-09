using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.AdminApi.DTOs.CateforyDtos
{
    public class CategoryGetListDto
    {
        public List<CategoryListItemDto> Categories { get; set; }
        public int  TotalCount { get; set; }
    }
}
