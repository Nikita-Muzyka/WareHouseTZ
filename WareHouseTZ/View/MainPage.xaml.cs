using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.ViewModal;

namespace WareHouseTZ.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModal vm)
        {
            InitializeComponent();
            BindingContext = vm;
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
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is MainPageViewModal viewModel)
            {
                viewModel.CancleToken();
            }
        }
    }
}
