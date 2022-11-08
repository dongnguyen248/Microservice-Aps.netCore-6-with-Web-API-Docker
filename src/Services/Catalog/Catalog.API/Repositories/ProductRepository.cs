using Catalog.API.Data;
using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.IRepository
{

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product>   _productCollection;
        public ProductRepository(IOptions<ProductStoreDatabaseSetting> productStoreDbSetting)
        {
            var mongoClient = new MongoClient(productStoreDbSetting.Value.ConnectionString);
            var mongoDataBaseName = mongoClient.GetDatabase(productStoreDbSetting.Value.DatabaseName);
            _productCollection = mongoDataBaseName.GetCollection<Product>(productStoreDbSetting.Value.ProductsCollectionName);

        }
        public async Task CreateProduct(Product product)
        {
            
            await _productCollection.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> deleteProduct = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult =  await _productCollection.DeleteOneAsync(deleteProduct);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> category = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _productCollection.Find(category).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _productCollection.Find(p=> p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _productCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productCollection.Find(p=>true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct = await _productCollection.ReplaceOneAsync(filter:g=>g.Id == product.Id,replacement:product);
            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
        }
    }
}
