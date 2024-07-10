using CRM.Library.Services;
using CRM.MAUI.ViewModels;
namespace CRM.MAUI.Views;

public partial class MarketConfigurationView : ContentPage
{
	public MarketConfigurationView()
	{
		InitializeComponent();
        BindingContext = new MarketConfigurationViewModel();
	}


    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as MarketConfigurationViewModel)?.UpdateContact();
    }

    private void BackToDashboard(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void TaxRateClicked(object sender, EventArgs e)
    {
        (BindingContext as MarketConfigurationViewModel).UpdateTaxRate();
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as MarketConfigurationViewModel)?.Refresh();
    }
}