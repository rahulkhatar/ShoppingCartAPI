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

namespace ShoppingCart.Core.CQRS_Services.Product_CQRS.Queries
{
    /*public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly ProductService _productService;
        public GetProductByIdQueryHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            return await _productService.GetById(request.Id);
        }
    }*/

    public class GetProductByIdQuery : IRequest<IResponseWrapper>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, IResponseWrapper>
    {
        private readonly ProductService _productService;
        public GetProductByIdQueryHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IResponseWrapper> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var product = await _productService.GetById(request.Id);
            if (product != null)
            {
                return await ResponseWrapper<Product?>.SuccessWithDataAsync(product, "Product details found.");
            }

            //throw new NotFoundException($"Product with id {request.Id} was not found.");
            return await ResponseWrapper.FailAsync("Failed to get the product details.");            
        }
    }
}
