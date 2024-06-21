using CRM.Library.Services;
using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
        BindingContext = new ShopViewModel();
	}

    private void BackToDashboard(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel).Refresh();
    }

    private void InventorySearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).Search();
    }

    private void PlaceInCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).PlaceInCart();
    }
}