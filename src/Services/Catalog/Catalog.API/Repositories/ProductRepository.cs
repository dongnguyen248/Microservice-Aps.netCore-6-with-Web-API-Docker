using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.IRepository
{

    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _productContext;
        readonly ILogger<ProductRepository> _productLog;
        public ProductRepository(IProductContext productContext, ILogger<ProductRepository> productLog, IConfiguration configuration)
        {
            _productLog = productLog;
            _productLog.LogInformation("start conncetstring");

            _productContext = productContext;
            //bool isMongoLive = _productContext.Products.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            
            


        }
        public async Task CreateProduct(Product product)
        {
            
            await _productContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> deleteProduct = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult =  await _productContext.Products.DeleteOneAsync(deleteProduct);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> category = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _productContext.Products.Find(category).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            
            return await _productContext.Products.Find(p=> p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _productContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                _productLog.LogInformation("start");
                DatabaseSeeding.SeedData(_productContext.Products);
                var products = await _productContext.Products.Find(p => true).ToListAsync();
                return products;
            }
            catch (Exception err)
            {
                _productLog.LogError(err.Message);
                throw;
            }
            //DatabaseSeeding.SeedData(_productCollection);
            
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct = await _productContext.Products.ReplaceOneAsync(filter:g=>g.Id == product.Id,replacement:product);
            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
        }
    }
}
