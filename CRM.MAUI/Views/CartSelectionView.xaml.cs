using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;

public partial class CartSelectionView : ContentPage
{
	public CartSelectionView()
	{
		InitializeComponent();
        BindingContext = new CartSelectionViewModel();
	}


    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartSelectionViewModel)?.Refresh();
    }

    private void SwitchCartClicked(object sender, EventArgs e)
    {
        (BindingContext as CartSelectionViewModel)?.SwitchCart();
    }

    private void BackToDashboard(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void NewCartClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewCart");
    }

}