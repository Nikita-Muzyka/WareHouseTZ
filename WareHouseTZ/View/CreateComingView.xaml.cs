using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class CreateComingView : ContentPage
{
	public CreateComingView(CreateComingViewModal vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if(BindingContext is CreateComingViewModal vm)
		{
			vm.LoadProductsCommand.Execute(null);
		}
    }
}