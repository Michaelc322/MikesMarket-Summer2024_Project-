using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;

public partial class NewCartView : ContentPage
{
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
}