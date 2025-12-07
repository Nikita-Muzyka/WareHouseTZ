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

namespace WareHouseTZ.ViewModal
{
    public partial class MainPageViewModal : ObservableObject
    {
        private readonly IDBService _dBService;
        public ObservableCollection<Product> Products { get;set; }
        public ObservableCollection<Product> FilteredProducts { get;set; }

        [ObservableProperty]
        public string searchText = string.Empty;
        public MainPageViewModal(IDBService dBService)
        {
            _dBService = dBService;
            LoadProducts();
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
        public async void LoadProducts()
        {
            var response = await _dBService.GetAllProductsDBAsync();
            var getallproducts = response as GetAllProductsResponse;
            if (getallproducts != null)
            {
                Products = getallproducts.Products;
            }


            FilteredProducts = new ObservableCollection<Product>(Products);
            OnPropertyChanged(nameof(FilteredProducts));
        }

        [RelayCommand]
        public async void EditProduct(Product product)
        {

        }
        [RelayCommand]
        public async void DeleteProduct(Product product)
        {

        }
        [RelayCommand]
        public async void AddProduct()
        {
            await Shell.Current.Navigation.PushAsync(new CreateProductView(_dBService));
        }
    }
}
