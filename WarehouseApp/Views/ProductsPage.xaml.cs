using WarehouseApp.ViewModels;

namespace WarehouseApp.Views;

public partial class ProductsPage : ContentPage
{

    private readonly ProductsVM _vm;
    public ProductsPage(ProductsVM vm)
    {
        _vm = vm;
        BindingContext = vm;
        InitializeComponent();
        this.Loaded += async (s, e) => await _vm.LoadProductsAsync();
    }


    private void AllRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var r = (RadioButton)sender;
        if(r.IsChecked)
        {
            RestsRadioButton.IsChecked = false;
            _vm.LoadProductsCommand.Execute(null);
        }
    }

    private void RestsRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var r = (RadioButton)sender;
        if(r.IsChecked)
        {
            AllRadioButton.IsChecked = false;
            _vm.FilterRestsCommand.Execute(null);
        }
    }
}