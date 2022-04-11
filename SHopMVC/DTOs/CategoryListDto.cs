using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHopMVC.DTOs
{
    public class CategoryListDto
    {
        public List<CategoryListItemDto> Categories { get; set; }
        public decimal TotalCount { get; set; }
    }
}
