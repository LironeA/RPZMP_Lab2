using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using WarehouseApp.Models;

namespace WarehouseApp.Services
{
    public class ProductService : IProductService
    {
        private readonly SQLiteAsyncConnection _dbConnection;

        public ProductService()
        {
            string mainDir = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "DataBase");
            if(!Directory.Exists(mainDir))
            {
                Directory.CreateDirectory(mainDir);
            }

            _dbConnection = new SQLiteAsyncConnection(Path.Combine(mainDir, "db.db"));
            _dbConnection.CreateTableAsync<Product>().Wait();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                return await _dbConnection.Table<Product>().ToListAsync();
            }
            catch
            {
                return new List<Product>();
            }
        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _dbConnection.InsertAsync(product);
            }
            catch
            {
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                await _dbConnection.UpdateAsync(product);
            }
            catch
            {
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _dbConnection.FindAsync<Product>(id);
                if(product != null)
                {
                    await _dbConnection.DeleteAsync(product);
                }
            }
            catch
            {
            }
        }
    }
}
