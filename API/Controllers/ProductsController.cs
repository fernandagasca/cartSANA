using Microsoft.AspNetCore.Mvc;
using ShoppingCartSANA.Domain.Entities;
using ShoppingCartSANA.Domain.Interfaces;

namespace ShoppingCartSANA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // 1. GET /api/products - Listar todos los productos
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products); 
        }

        // 2. GET /api/products/{id} - Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); 
            }
            return Ok(product); 
        }

        // 3. POST /api/products - Crear un nuevo producto
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product); 
        }

        // 4. PUT /api/products/{id} - Actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("El ID del producto no coincide con el ID de la URL."); 
            }

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(); 
            }

            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        // 5. DELETE /api/products/{id} - Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); 
            }

            await _productRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
