using Microsoft.Extensions.Logging;
using ShoppingCart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastucture.Data
{
    public class ProductDataSource
    {
        private readonly string _filePath;
        private List<Product> _products;
        private ILogger _logger;
        private readonly object _lock = new object();

        public ProductDataSource(string filePath, ILogger logger)
        {
            _filePath = filePath;
            _products = new List<Product>();
            _logger = logger;
            LoadProductsFromFile();
        }

        private void LoadProductsFromFile()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error deserializing products from file: {ex.Message}");
                    _products = new List<Product>(); // Initialize with empty list on error
                }
            }
            else
            {
                _products.AddRange(new List<Product>
                {
                    new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "Powerful laptop for work and gaming", Price = 1200.00m, Stock = 50 },
                    new Product { Id = Guid.NewGuid(), Name = "Mouse", Description = "Wireless ergonomic mouse", Price = 25.00m, Stock = 200 },
                    new Product { Id = Guid.NewGuid(), Name = "Keyboard", Description = "Mechanical gaming keyboard", Price = 75.00m, Stock = 100 }
                });
                SaveProductsToFile();
            }
        }

        private void SaveProductsToFile()
        {
            lock (_lock)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_products, options);
                File.WriteAllText(_filePath, json);
            }
        }

        public IQueryable<Product> GetAllProducts()
        {
            lock (_lock)
            {
                return _products.AsQueryable();
            }
        }

        public Product? GetProductById(Guid id)
        {
            lock (_lock)
            {
                return _products.FirstOrDefault(p => p.Id == id);
            }
        }

        public void AddProduct(Product product)
        {
            lock (_lock)
            {
                // Simple ID generation
               // product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
                _products.Add(product);
                SaveProductsToFile();
            }
        }

        public void UpdateProduct(Product product)
        {
            lock (_lock)
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    SaveProductsToFile();
                }
            }
        }

        public void DeleteProduct(Guid id)
        {
            lock (_lock)
            {
                var productToRemove = _products.FirstOrDefault(p => p.Id == id);
                if (productToRemove != null)
                {
                    _products.Remove(productToRemove);
                    SaveProductsToFile();
                }
            }
        }
    }
}
