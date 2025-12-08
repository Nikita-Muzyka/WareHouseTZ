using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class ComingView : ContentPage
{
	ComingViewModel comingVM;
	public ComingView(IDBService dBService,IDisplayService display)
	{
		InitializeComponent();
		comingVM = new ComingViewModel(dBService,display);
		BindingContext = comingVM;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if(BindingContext is ComingViewModel comingVM)
		{
			comingVM.LoadComingsCommand.Execute(comingVM);
		}
    }
}