using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using FeedTheCrowd.Dtos.Products;

namespace FeedTheCrowd.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
       /* // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }*/

        // GET: api/Products/5                                                                            
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("{id}/recipe")]
        public async Task<IActionResult> GetProductsForRecipe(int id)
        {
            var products = await _productService.GetByRecipeId(id);
            return Ok(products);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewProductDto[] newProducts)
        {
            if(id <= 0)
            {
                return NotFound();
            }
            await _productService.Update(id, newProducts);
            return NoContent();
        }

    }
}
