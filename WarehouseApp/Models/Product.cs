using SQLite;
using System.Text.Json.Serialization;

namespace WarehouseApp.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement, JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        public ProductObservableModel ToObservableModel()
        {
            return new ProductObservableModel
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Quantity = Quantity
            };
        }
    }
}
