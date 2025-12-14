using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class CreateProductView : ContentPage
{
	public CreateProductView(CreateProductViewModal vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
		if(BindingContext is CreateProductViewModal vm)
		{
			vm.CancelToken();
		}
    }
}