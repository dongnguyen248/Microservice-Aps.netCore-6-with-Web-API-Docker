using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class ProductStoreDatabaseSetting: IProductContext
    {
        public ProductStoreDatabaseSetting(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }


        public IMongoCollection<Product> Products { get; }
    }
}
