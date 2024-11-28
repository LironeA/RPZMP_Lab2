using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WarehouseApp.Models;
using System.Collections.ObjectModel;
using WarehouseApp.Views;

namespace WarehouseApp.ViewModels
{
    public partial class ProductsVM : ObservableObject
    {
        private readonly IProductService _productService;

        public ProductsVM(IProductService productService)
        {
            _productService = productService;
            Products = new ObservableCollection<ProductObservableModel>();
        }

        [ObservableProperty]
        private ObservableCollection<ProductObservableModel> _products;

        [ObservableProperty] private decimal _maxPrice;
        [ObservableProperty] private decimal _minPrice;

        [RelayCommand]
        public async Task LoadProductsAsync()
        {
            var productsFromApi = await _productService.GetProductsAsync();
            Products.Clear();

            foreach(var product in productsFromApi)
            {
                Products.Add(product.ToObservableModel());
            }

            if(Products.Count > 0)
            {
                MaxPrice = Products.Max(x => x.Price);
                MinPrice = Products.Min(x => x.Price);
            }
        }

        [RelayCommand]
        public async Task FilterRests()
        {
            var productsFromApi = await _productService.GetProductsAsync();
            Products.Clear();

            foreach(var product in productsFromApi.Where(x => x.Quantity < 5))
            {
                Products.Add(product.ToObservableModel());
            }

            if(Products.Count > 0)
            {
                MaxPrice = Products.Max(x => x.Price);
                MinPrice = Products.Min(x => x.Price);
            }

        }

        [RelayCommand]
        public async Task AddProduct()
        {
            var product = new ProductObservableModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductFormPage(product, AddCallBack, null));
            
        }

        public async Task AddCallBack(ProductObservableModel product)
        {
            await _productService.AddProductAsync(product.ToModel());
            Products.Add(product);
        }

        [RelayCommand]
        public async Task UpdateProduct(ProductObservableModel product)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductFormPage(product, UpdateCallBack, DeleteCallBack));
        }

        public async Task UpdateCallBack(ProductObservableModel product)
        {
            await _productService.UpdateProductAsync(product.ToModel());
            //Products.Add(product);
        }

        public async Task DeleteCallBack(ProductObservableModel product)
        {
            await _productService.DeleteProductAsync(product.Id);
            Products.Remove(product);
        }


        [RelayCommand]
        public async Task DeleteProductAsync(ProductObservableModel product)
        {
            if(product != null)
            {
                await _productService.DeleteProductAsync(product.Id);
                Products.Remove(product);
            }
        }
    }
}
