using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;

namespace WarehouseApp.Models
{
    public partial class ProductObservableModel : ObservableObject
    {
        [ObservableProperty] private int _id;
        [ObservableProperty] private string _name;
        [ObservableProperty] private decimal _price;
        [ObservableProperty] private int _quantity;

        public Product ToModel()
        {
            return new Product
            {
                Id = this.Id,
                Name = this.Name,
                Price = this.Price,
                Quantity = this.Quantity
            };
        }
    }
}
