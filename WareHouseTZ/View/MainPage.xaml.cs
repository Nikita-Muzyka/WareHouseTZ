using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Автоматическая подгрузка при каждом входе на страницу
            if (BindingContext is MainPageViewModal viewModel)
            {
                viewModel.LoadProductsCommand.Execute(null);
            }
        }
    }
}
