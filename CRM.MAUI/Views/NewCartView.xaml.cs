using CRM.Library.Models;
using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;


[QueryProperty(nameof(CartID), "cartId")]
public partial class NewCartView : ContentPage
{
    public int CartID { get; set; }

    public NewCartView()
	{
		InitializeComponent();
        BindingContext = new NewCartViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CartSelection");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as NewCartViewModel).Add();
        Shell.Current.GoToAsync("//CartSelection");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new NewCartViewModel(CartID);
    }
}