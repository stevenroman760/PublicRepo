using PublicRepo.ViewModels;

namespace PublicRepo.Views;

public partial class DashboardView : ContentPage
{
	public DashboardView()
	{
		InitializeComponent();
		this.BindingContext = new DashboardViewModel();
	}
}