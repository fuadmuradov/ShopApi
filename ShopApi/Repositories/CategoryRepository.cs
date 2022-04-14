using Microsoft.EntityFrameworkCore;
using ShopApi.Data.DAL;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public class CategoryRepository
    {
        private readonly ShopDbContext context;

        public CategoryRepository(ShopDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Category category)
        {
             await context.AddAsync(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync(); 
        }

        public async Task Remove(Category category)
        {
             context.Categories.Remove(category);
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

    }
}
