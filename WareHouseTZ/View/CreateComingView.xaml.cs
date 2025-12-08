using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class CreateComingView : ContentPage
{
	public CreateComingView(IDBService dBService,IDisplayService display)
	{
		InitializeComponent();
		BindingContext = new CreateComingViewModal(dBService,display);
	}
}