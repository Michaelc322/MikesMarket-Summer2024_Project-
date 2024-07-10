using CRM.Library.Models;
using CRM.MAUI.ViewModels;

namespace CRM.MAUI.Views;



public partial class SubscriptionsView : ContentPage
{
	public SubscriptionsView()
	{
		InitializeComponent();
        BindingContext = new SubscriptionViewModel();
	}

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as SubscriptionViewModel)?.AddSubscription();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as SubscriptionViewModel)?.Refresh();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void BackToDashboard(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}