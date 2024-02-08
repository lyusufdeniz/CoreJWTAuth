using Auth.Core.Entities;
using Auth.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IGenericService<Product,ProductDTO> _productService;

        public ProductController(IGenericService<Product,ProductDTO> productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _productService.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO product)
        {
            return ActionResultInstance(await _productService.AddAsync(product));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            return ActionResultInstance(await _productService.Update(product.Id,product));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ActionResultInstance(await _productService.Remove(id));
        }

    }
}
