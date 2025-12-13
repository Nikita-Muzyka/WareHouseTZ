using WareHouseTZ.View;

namespace WareHouseTZ
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateProductView),typeof(CreateProductView));
            Routing.RegisterRoute(nameof(EditProductView), typeof(CreateProductView));
        }
    }
}
