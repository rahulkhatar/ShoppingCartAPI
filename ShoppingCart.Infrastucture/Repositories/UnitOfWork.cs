using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastucture.Repositories
{    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _context;
        private bool isSaveChanges = false;
        public IProductRepository Prod { get; }

        public UnitOfWork(ProductDbContext context, IProductRepository prod)
        {
            _context = context;
            Prod = prod;
        }        

        public async Task<bool> SaveChanges()
        {
            await _context.SaveChangesAsync();
            isSaveChanges = true;
            return isSaveChanges;
        }      
    }
}
