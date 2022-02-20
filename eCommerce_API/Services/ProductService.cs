using eCommerce_API.Models.CreateModels;
using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.UpdateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_API.Services
{
    public interface IProductService
    {
        Task<List<ProductDisplayModel>> GetProductsAsync();
        Task<ActionResult<ProductDisplayModel>> GetProductAsync(int id);
        Task<IActionResult> UpdateProductAsync(int id, ProductUpdateModel updateProduct);
        Task<ActionResult<ProductDisplayModel>> CreateProductAsync(ProductCreateModel createProduct);
        Task<IActionResult> DeleteProductAsync(int id);
    }
    public class ProductService : ControllerBase, IProductService
    {
        private readonly SqlContext _context;

        public ProductService(SqlContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDisplayModel>> GetProductsAsync()
        {
            List<ProductDisplayModel> products = new();
            var existingProduct = await _context.Products
                .Include(x => x.Category).ToListAsync();

            foreach (var product in existingProduct)
                products.Add(new ProductDisplayModel(
                    product.Id,
                    product.Barcode,
                    product.ProductName,
                    product.ProductDescription,
                    product.Price,
                    product.Category.CategoryName));

            return products;
        }

        public async Task<ActionResult<ProductDisplayModel>> GetProductAsync(int id)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
                return BadRequest("Product with that ID dont exist.");

            return new ProductDisplayModel(
                product.Id,
                product.Barcode,
                product.ProductName,
                product.ProductDescription,
                product.Price,
                product.Category.CategoryName);
        }

        public async Task<IActionResult> UpdateProductAsync(int id, ProductUpdateModel updateProduct)
        {
            try
            {
                var existingProduct = await _context.Products
                .Include(x => x.Category)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                if (existingProduct == null)
                    return BadRequest("Your input info was incorrect.");

                CategoryEntity? category = new CategoryEntity();
                category = await _context.Categories
                    .Where(x => x.CategoryName == updateProduct.CategoryName)
                    .FirstOrDefaultAsync();

                if (category == null && updateProduct.CategoryName != null)
                {
                    category = new(updateProduct.CategoryName);
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                    existingProduct.Category = category;
                }

                if (!string.IsNullOrEmpty(updateProduct.ProductName))
                    existingProduct.ProductName = updateProduct.ProductName;
                if (!string.IsNullOrEmpty(updateProduct.ProductDescription))
                    existingProduct.ProductDescription = updateProduct.ProductDescription;
                if (updateProduct.Price != 0)
                    existingProduct.Price = updateProduct.Price;

                _context.Entry(existingProduct).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch 
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult<ProductDisplayModel>> CreateProductAsync(ProductCreateModel createProduct)
        {
            try
            {
                CategoryEntity? category = new CategoryEntity();
                category = await _context.Categories
                    .Where(x => x.CategoryName == createProduct.CategoryName)
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    if (string.IsNullOrEmpty(createProduct.CategoryName))
                    {
                        var nullCategory = await _context.Categories
                            .Where(x => x.CategoryName == "Unknown").FirstOrDefaultAsync();

                        if (nullCategory == null)
                            category = new("Unknown");
                        else
                            category = nullCategory;
                    }
                    else
                        category = new(createProduct.CategoryName);

                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                }

                var product = new ProductEntity(
                    createProduct.Barcode,
                    createProduct.ProductName,
                    createProduct.ProductDescription,
                    createProduct.Price,
                    category.Id);

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                var createdProduct = new ProductDisplayModel(
                    product.Id, createProduct.Barcode,
                    createProduct.ProductName,
                    createProduct.ProductDescription,
                    createProduct.Price,
                    createProduct.CategoryName);

                return CreatedAtAction("GetProduct", new { id = createdProduct.Id }, createdProduct);
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var productDelete = await _context.Products.FindAsync(id);
            if (productDelete == null)
                return BadRequest("Product with that ID dont exist.");

            _context.Products.Remove(productDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
