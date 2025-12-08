using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class CreateProductView : ContentPage
{
	public CreateProductView(IDBService dBService,IDisplayService display)
	{
		InitializeComponent();
		BindingContext = new CreateProductViewModal(dBService,display);
	}
}