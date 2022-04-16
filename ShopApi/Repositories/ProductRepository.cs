using ShopApi.Data.DAL;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        private readonly ShopDbContext context;

        public ProductRepository(ShopDbContext context):base(context)
        {
            this.context = context;
        }
    }
}
