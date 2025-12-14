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
    public partial class CreateComingViewModal : ObservableObject
    {
        private readonly IDBService _dBService;
        private readonly IDisplayService _display;
        private CancellationTokenSource _cts;
        public CreateComingViewModal(IDBService dBService, IDisplayService display)
        {
            _dBService = dBService;
            _display = display;
            _cts = new CancellationTokenSource();
        }

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

        public ObservableCollection<Product> Products { get; } = new();

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

                if (getall?.Products != null)
                {
                    foreach (var product in getall.Products)
                    {
                        token.ThrowIfCancellationRequested();
                        Products.Add(product);
                    }
                }
            }
            catch (OperationCanceledException ex) { }
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

                //var response = await _dBService.AddComingDBAsync(coming);
                //_display.ShowMessage(response.Message);

                ChangeUnit(coming);
                //response = await _dBService.UpdateProductAsync(SelectedProduct);
            }
            catch (OperationCanceledException ex) { }
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

