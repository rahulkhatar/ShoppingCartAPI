using Microsoft.EntityFrameworkCore;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;
using ShoppingCart.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastucture.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<Product?> GetByName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return product;
        }
    }
}
