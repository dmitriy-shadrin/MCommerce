using MCommerce.Catalog.API.DAL.Contracts;
using MCommerce.Catalog.API.Entities;
using MCommerce.Catalog.API.Repositories.Contracts;
using MongoDB.Driver;

namespace MCommerce.Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(p => p.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await (await _context.Products.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await (await _context.Products.FindAsync(p => true)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await (await _context.Products.FindAsync(filter)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await (await _context.Products.FindAsync(filter)).ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);            

            var updateResult = await _context.Products.ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
