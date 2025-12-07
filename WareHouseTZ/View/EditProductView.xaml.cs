using WareHouseTZ.ViewModal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.Modal;
namespace WareHouseTZ.View;

public partial class EditProductView : ContentPage
{
	public EditProductView(IDBService dBService, IDisplayService display,Product product)
	{
		InitializeComponent();
        BindingContext = new EditProductViewModal(dBService, display,product);
    }
}