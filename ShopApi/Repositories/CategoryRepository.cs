using Microsoft.EntityFrameworkCore;
using ShopApi.Data.DAL;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public class CategoryRepository:Repository<Category>, ICategoryRepository
    {
        private readonly ShopDbContext context;

        public CategoryRepository(ShopDbContext context):base(context)
        {
            this.context = context;
        }

       
    }
}
