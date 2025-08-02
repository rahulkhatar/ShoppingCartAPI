using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.CQRS_Services.Product_CQRS.Commands;
using ShoppingCart.Core.CQRS_Services.Product_CQRS.Queries;
using ShoppingCart.Core.DTOs;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;
using ShoppingCart.Core.Services;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ILogger<ProductController> _logger;
        private readonly IMediator _mediatR;
        
        public ProductController(ILogger<ProductController> logger, IMediator mediatR)
        {
            _logger = logger;
            _mediatR = mediatR;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediatR.Send(new GetProductListQuery());
            if (response.IsSuccessful)
                return Ok(response);
            else
                return BadRequest(response.Messages);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediatR.Send((new GetProductByIdQuery { Id = id }));
            if (response.IsSuccessful)
                return Ok(response);
            else
                return BadRequest(response.Messages);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(ProductRequest product)
        {
            var response = await _mediatR.Send(new AddProductCommand { Product = product });
            if (response.IsSuccessful)
                return Ok(response);
            else
                return BadRequest(response.Messages);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductRequest product)
        {
            var response = await _mediatR.Send(new UpdateProductCommand { Id = id, Product = product });
            if (response.IsSuccessful)
                return Ok(response);
            else
                return BadRequest(response.Messages);
        }
        
        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _mediatR.Send(new DeleteProductCommand { Id = id });
            if (response.IsSuccessful)
                return Ok(response);
            else
                return BadRequest(response.Messages);
        }

        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            return Ok();
        }
    }
}
