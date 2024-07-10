using CRM.Library.Models;
using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;


[QueryProperty(nameof(ProductId), "productId")]

public partial class ConfigureProductView : ContentPage
{
	public ConfigureProductView()
	{
		InitializeComponent();
	}


    public int ProductId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MarketConfig");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductConfigViewModel).Add();
        Shell.Current.GoToAsync("//MarketConfig");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductConfigViewModel(ProductId);
    }
}