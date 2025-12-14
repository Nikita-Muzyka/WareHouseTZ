using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WareHouseTZ.Modal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;

namespace WareHouseTZ.ViewModal
{
    public partial class CreateComingViewModal : BaseViewModel
    {

        private CancellationTokenSource _cts;
        public CreateComingViewModal(IDBService dBService, IDisplayService display) :base(dBService,display)
        { 
           _cts = new CancellationTokenSource();
        }

        public ObservableCollection<Product> Products { get; set; }


        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private Product selectedProduct;

        [ObservableProperty]
        private decimal quantity = 1;

        [ObservableProperty]
        private decimal? price;

        [ObservableProperty]
        private string supplier;

        [ObservableProperty]
        private string document;

        public decimal Total => (price ?? 0) * quantity;

        [RelayCommand]
        public async Task LoadProducts()
        {
            await LoadAsync(_cts.Token);
        }
        private async Task LoadAsync(CancellationToken token)
        {
            try
            {
                var response = await _dBService.GetAllProductsDBAsync(token);
                var getall = response as GetAllProductsResponse<Product>;
  
                    Products = getall.Products != null 
                        ? new ObservableCollection<Product>(getall.Products) 
                        : new ObservableCollection<Product>();
                    OnPropertyChanged(nameof(Products));
            }
            catch (OperationCanceledException) { }
        }
        [RelayCommand]
        private async Task CreateComing()
        {
            await Create(_cts.Token);
        }
        private async Task Create(CancellationToken token)
        {
            try
            {
                if (selectedProduct == null || quantity <= 0)
                {
                    _display.ShowMessage("Заполните обязательные поля");
                    return;
                }

                var coming = new Coming
                {
                    Date = date,
                    Product_Id = selectedProduct.Id,
                    ProductName = selectedProduct.Name,
                    Quantity = quantity,
                    Price = price,
                    Supplier = supplier,
                    Document = document
                };

                var response = await _dBService.AddComingDBAsync(coming,token);
                _display.ShowMessage(response.Message);
                token.ThrowIfCancellationRequested();
                ChangeUnit(coming);
                response = await _dBService.UpdateProductAsync(SelectedProduct, token);
            }
            catch (OperationCanceledException) { }
        }
        void ChangeUnit(Coming coming)
        {
            string pattern = @"\d+"; 
            Match match = Regex.Match(SelectedProduct.Unit, pattern);

            if (match.Success)
            {
                decimal number = decimal.Parse(match.Value); 
                decimal result = number + coming.Quantity;

                // Сохраняем единицу измерения
                string unit = SelectedProduct.Unit.Substring(match.Length).Trim(); 

                string newText = $"{result} {unit}"; 
                SelectedProduct.Unit = newText;
            }
        }
        public void CancleToken()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }

}

