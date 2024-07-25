using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
	}

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Refresh();
    }

    private void InlineDelete_Clicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Refresh();
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Edit();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.DeleteProduct();

    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }



    private void BackToDashboard(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}