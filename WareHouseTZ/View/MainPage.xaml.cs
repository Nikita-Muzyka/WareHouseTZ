using WareHouseTZ.Service;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModal mainVM;
        public MainPage(IDBService dBService)
        {
            InitializeComponent();
            mainVM = new MainPageViewModal(dBService);
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
