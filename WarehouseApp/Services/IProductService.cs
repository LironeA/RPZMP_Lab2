using System;
using System.Linq;
using WarehouseApp.Models;

namespace WarehouseApp.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<List<Product>> GetProductsAsync();
        Task UpdateProductAsync(Product product);
    }
}
