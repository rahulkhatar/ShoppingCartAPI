using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Services
{
    public class ProductService
    {
        private ILogger<ProductService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string? _someSecretKey;

        public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _someSecretKey = configuration["AppSecrets:SomeSecretKey"];
        }

        public async Task<IEnumerable<Product>> GetList()
        {
            IEnumerable<Product> productList = await _unitOfWork.Prod.GetAll();
            return productList;
        }

        public async Task<Product?> GetById(Guid id)
        {            
            Product? product = await _unitOfWork.Prod.GetById(id);
            if(product == null)
                throw new NotFoundException($"Product with id {id} was not found.");
            return product;
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            Product newProduct = await _unitOfWork.Prod.Add(product);
            bool responseStatus = await _unitOfWork.SaveChanges();
            return newProduct;           
        }

        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            (bool status, Product updateProduct) = await _unitOfWork.Prod.Update(id, product);
            bool responseStatus = await _unitOfWork.SaveChanges();
            return updateProduct;
        }

        public async Task<Product?> DeleteProduct(Guid id)
        {
            Product? product = await _unitOfWork.Prod.GetById(id);
            bool isDeleted = await _unitOfWork.Prod.Delete(id);
            bool responseStatus = await _unitOfWork.SaveChanges();
            return product;
        }

        public async Task<Product?> GetByName(string name)
        {
            Product? product = await _unitOfWork.Prod.GetByName(name);
            return product;
        }
    }
}
