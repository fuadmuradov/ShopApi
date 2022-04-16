using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SHopMVC.DTOs
{
    public class CategoryPostDto
    {
        [Required]
        public string Name { get; set; }
    }
}
