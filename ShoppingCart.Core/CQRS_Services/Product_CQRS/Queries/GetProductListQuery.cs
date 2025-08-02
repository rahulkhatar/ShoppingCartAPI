using MediatR;
using ShoppingCart.Core.Interfaces;
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
    /*public class GetProductListQuery : IRequest<IEnumerable<Product>>
    {
    }
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<Product>>
    {
        private readonly ProductService _productService;

        public GetProductListQueryHandler(ProductService productService)
        {
           _productService = productService;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {            
            return await _productService.GetList();
        }
    }*/

    public class GetProductListQuery : IRequest<IResponseWrapper>
    {
    }
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IResponseWrapper>
    {
        private readonly ProductService _productService;

        public GetProductListQueryHandler(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IResponseWrapper> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productService.GetList();
            if (productList.Count() > 0)
            {
                return await ResponseWrapper<IEnumerable<Product>>.SuccessWithTotalCountAsync(productList, productList.Count());
            }

            return await ResponseWrapper.FailAsync("Failed to get the product list.");
        }
    }
}
