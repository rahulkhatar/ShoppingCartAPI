using MediatR;
using ShoppingCart.Core.Models;
using ShoppingCart.Core.Services;
using ShoppingCart.Core.Wrapper.Interface;
using ShoppingCart.Core.Wrapper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.CQRS_Services.Product_CQRS.Commands
{
    /*public class DeleteProductCommand : IRequest<Product>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Product?>
    {
        public ProductService _productService;
        public DeleteProductCommandHandler(ProductService productService) 
        {
            _productService = productService;
        }

        public async Task<Product?> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            return await _productService.DeleteProduct(request.Id);
        }
    }*/

    public class DeleteProductCommand : IRequest<IResponseWrapper>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IResponseWrapper>
    {
        public ProductService _productService;
        public DeleteProductCommandHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IResponseWrapper> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var product = await _productService.DeleteProduct(request.Id);
            if (product != null)
            {
                return await ResponseWrapper<Product?>.SuccessWithDataAsync(product, "Product deleted successfully.");
            }

            return await ResponseWrapper.FailAsync("Failed to delete the product.");            
        }
    }
}
