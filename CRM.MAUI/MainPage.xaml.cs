namespace CRM.MAUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//CartSelection");
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManager");
        }

        private void ConfigClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MarketConfig");
        }

        private void SubscriptionClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Subscriptions");
        }
    }

}
