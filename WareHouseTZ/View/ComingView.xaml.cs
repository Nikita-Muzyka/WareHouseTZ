using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View;

public partial class ComingView : ContentPage
{
	public ComingView(ComingViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		if(BindingContext is ComingViewModel comingVM)
		{
			comingVM.LoadComingsCommand.Execute(comingVM);
		}
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is ComingViewModel comingVM)
        {
            comingVM.CancelToken();
        }
    }
}