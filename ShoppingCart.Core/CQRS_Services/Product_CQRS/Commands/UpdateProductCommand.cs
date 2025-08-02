using AutoMapper;
using MediatR;
using ShoppingCart.Core.DTOs;
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
    /*public class UpdateProductCommand : IRequest<Product>
    {
        public Guid Id { get; set; }
        public ProductRequest? Product { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly ProductService _productService;
        private IMapper _mapper;

        public UpdateProductCommandHandler(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken ct)
        {

            return await _productService.UpdateProduct(request.Id, _mapper.Map<Product>(request.Product));
        }
    }*/

    public class UpdateProductCommand : IRequest<IResponseWrapper>
    {
        public Guid Id { get; set; }
        public ProductRequest? Product { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResponseWrapper>
    {
        private readonly ProductService _productService;
        private IMapper _mapper;

        public UpdateProductCommandHandler(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var product = await _productService.UpdateProduct(request.Id, _mapper.Map<Product>(request.Product));
            if (product != null)
            {
                return await ResponseWrapper<Product?>.SuccessWithDataAsync(product, "Product updated successfully.");
            }

            return await ResponseWrapper.FailAsync("Product update failed.");            
        }
    }
}
