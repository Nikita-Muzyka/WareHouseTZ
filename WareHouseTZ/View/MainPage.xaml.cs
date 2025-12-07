using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModal mainVM;
        public MainPage(IDBService dBService,IDisplayService display)
        {
            InitializeComponent();
            mainVM = new MainPageViewModal(dBService,display);
            BindingContext = mainVM;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Запускаем команду загрузки
        //    if (BindingContext is MainPageViewModal viewModel)
        //    {
        //        viewModel.LoadProductsCommand.Execute(null);
        //    }
        //}
    }
}
