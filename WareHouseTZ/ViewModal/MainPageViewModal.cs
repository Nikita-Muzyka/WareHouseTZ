using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Service;
using WareHouseTZ.Modal;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using WareHouseTZ.View;
using WareHouseTZ.Service.Display;

namespace WareHouseTZ.ViewModal
{
    public partial class MainPageViewModal : BaseViewModel
    {
        private CancellationTokenSource _cts;

        public ObservableCollection<Product> Products { get;set; }
        public ObservableCollection<Product> FilteredProducts { get;set; }

        [ObservableProperty]
        public string searchText = string.Empty;
        public MainPageViewModal(IDBService dBService, IDisplayService display) : base(dBService, display)
        { 
            _cts = new CancellationTokenSource();
        }

        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredProducts = new ObservableCollection<Product>(Products);
                OnPropertyChanged(nameof(FilteredProducts));
            }
            else
            { 
               var listproduct = Products.Where(c => c.Name.ToLowerInvariant().Contains(value.ToLowerInvariant()) ||
                c.Unit.Contains(value));
                FilteredProducts = new ObservableCollection<Product>(listproduct);
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }
        [RelayCommand]
        public async Task LoadProducts()
        {
            await LoadAsync(_cts.Token);
        }
        public async Task LoadAsync(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                var response = await _dBService.GetAllProductsDBAsync(token);
                if(response.Success == true)
                {
                    var getallproducts = response as GetAllProductsResponse<Product>;

                    FilteredProducts =
                    new ObservableCollection<Product>(getallproducts.Products) ??
                    new ObservableCollection<Product>();

                    token.ThrowIfCancellationRequested();
                    OnPropertyChanged(nameof(FilteredProducts));
                }
                else
                {
                    _display.ShowMessage(response.Message);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { }
        }

        [RelayCommand]
        public async Task EditProduct(Product ProductParm)
        {
            await Shell.Current.GoToAsync(nameof(EditProductView), new Dictionary<string, object> { ["ProductParm"] = ProductParm });
        }
        [RelayCommand]
        public async Task DeleteProduct(Product product)
        {
            await DeleteAsync(product, _cts.Token);
        }
        public async Task DeleteAsync(Product product, CancellationToken token)
        {
            try
            {
                var response = await _dBService.DeleteProductAsync(product.Id, token);
                _display.ShowMessage(response.Message);
                if (response.Success == true) await LoadProducts();
            }
            catch (OperationCanceledException ex) { }
            {

            }
        }
        [RelayCommand]
        public async Task AddProduct()
        {
            await Shell.Current.GoToAsync(nameof(CreateProductView));
        }
        public void CancleToken()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }
}
