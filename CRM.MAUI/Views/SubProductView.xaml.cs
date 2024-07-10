using CRM.Library.Models;
using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;


[QueryProperty(nameof(ProductId), "productId")]
[QueryProperty(nameof(CartId), "cartId")]


public partial class SubProductView : ContentPage
{
	public SubProductView()
	{
		InitializeComponent();
	}


    public int ProductId { get; set; }
    public int CartId { get; set; }


    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Subscriptions");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as SubProductViewModel).Add();
        Shell.Current.GoToAsync("//Subscriptions");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new SubProductViewModel(ProductId, CartId);
    }
}