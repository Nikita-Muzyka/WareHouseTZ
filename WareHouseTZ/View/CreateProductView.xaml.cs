using WareHouseTZ.Service;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class CreateProductView : ContentPage
{
	public CreateProductView(IDBService dBService)
	{
		InitializeComponent();
		BindingContext = new CreateProductViewModal(dBService);
	}
}