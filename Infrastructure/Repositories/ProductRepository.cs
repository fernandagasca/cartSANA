using Microsoft.EntityFrameworkCore;
using ShoppingCartSANA.Domain.Entities;
using ShoppingCartSANA.Domain.Interfaces;
using ShoppingCartSANA.Infrastructure.Persistence;

namespace ShoppingCartSANA.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductsCategories) 
                .ThenInclude(pc => pc.Category)    
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductsCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddCategoryToProductAsync(int productId, int categoryId)
        {
            var productCategory = new ProductsCategories
            {
                Product_Id = productId,
                Category_Id = categoryId
            };

            await _context.ProductsCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();
        }


        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            // Actualizar las propiedades de la entidad existente
            existingProduct.Name_Product = product.Name_Product;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Image = product.Image;

            // Guardar cambios
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
