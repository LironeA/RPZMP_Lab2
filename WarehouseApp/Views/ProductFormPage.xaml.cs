using WarehouseApp.Models;

namespace WarehouseApp.Views;

public partial class ProductFormPage : ContentPage
{
    public ProductObservableModel Product { get; set; }
    public Func<ProductObservableModel, Task> AddCallBack { get; set; }
    public Func<ProductObservableModel, Task> DeleteCallBack { get; set; }

    public ProductFormPage(ProductObservableModel product, Func<ProductObservableModel, Task> addCallBack, Func<ProductObservableModel, Task> deleteCallBack)
    {
        Product = product;
        AddCallBack = addCallBack;
        DeleteCallBack = deleteCallBack;
        InitializeComponent();

        NameEntry.Text = Product.Name ?? string.Empty;
        PriceEntry.Text = Product.Price.ToString();
        QuantityEntry.Text = Product.Quantity.ToString();
    }

    private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Product.Name = e.NewTextValue;
    }

    private void PriceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try {
            Product.Price = decimal.Parse(e.NewTextValue);
        }
        catch (Exception ex)
        {
            Product.Price = 0;
        }
    }

    private void QuantityEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            Product.Quantity = int.Parse(e.NewTextValue);
        }
        catch(Exception ex)
        {
            Product.Quantity = 0;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await AddCallBack.Invoke(Product);
            await Application.Current.MainPage.Navigation.PopAsync();

        });
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if(DeleteCallBack is not null)
            {
                DeleteCallBack.Invoke(Product);
            }
            Application.Current.MainPage.Navigation.PopAsync();
        });

    }
}