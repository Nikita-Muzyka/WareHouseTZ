using WareHouseTZ.ViewModal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.Modal;
namespace WareHouseTZ.View;

public partial class EditProductView : ContentPage
{
	public EditProductView(EditProductViewModal vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is EditProductViewModal vm)
        {
            vm.LoadProductCommand.Execute(null);
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is EditProductViewModal vm)
        {
            vm.CancelToken();
        }
    }
}