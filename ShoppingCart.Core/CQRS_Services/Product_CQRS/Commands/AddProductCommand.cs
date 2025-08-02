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
    /*public class AddProductCommand : IRequest<Product>
    {
        public ProductRequest? Product { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken ct)
        {
            return await _productService.AddProduct(_mapper.Map<Product>(request.Product));
        }
    }*/

    public class AddProductCommand : IRequest<IResponseWrapper>
    {
        public ProductRequest? Product { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, IResponseWrapper>
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> Handle(AddProductCommand request, CancellationToken ct)
        {
            var product = await _productService.AddProduct(_mapper.Map<Product>(request.Product));
            if (product != null)
            {
                return await ResponseWrapper<Product?>.SuccessWithDataAsync(product, "New product inserted.");
            }

            return await ResponseWrapper.FailAsync("New product fail to add in the database.");            
        }
    }
}
