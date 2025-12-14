using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Modal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;


namespace WareHouseTZ.ViewModal
{
    [QueryProperty(nameof(ProductParm),"ProductParm")]
    public partial class EditProductViewModal : BaseViewModel
    {
        private ProductValidation _validation;
        private CancellationTokenSource _cts;

        public bool HasErrors => _validation.HasErrors;
        public string NameError => _validation.GetErrors("NameError") as String;
        public string CountError => _validation.GetErrors("CountError") as String;
        [ObservableProperty]
        Product productParm;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        public string selectedUnit = "шт"; // Значение по умолчанию
        [ObservableProperty]
        public string count;

        [ObservableProperty]
        public List<string> units = new List<string>
        {
            "шт", "кг", "г",
        };
        public EditProductViewModal(IDBService dBService, IDisplayService display) : base(dBService, display)
        {
            _validation = new ProductValidation(dBService);
            _validation.ErrorsChanged += (s, e) => EventInvoke(e);
            _cts = new CancellationTokenSource();
        }
        [RelayCommand]
        void LoadProduct()
        {
            Name = ProductParm.Name;
            Description = ProductParm.Description;

            string text = ProductParm.Unit.TrimEnd(new char[] { ' ', 'ш', 'т', 'к', 'г' });

            Count = text;
        }

        [RelayCommand]
        public async Task EditProduct()
        {
            _validation.EditValidationAll(Name, Count, ProductParm.Name,_cts.Token);
            _cts.Token.ThrowIfCancellationRequested();
            if (HasErrors == false)
            {
                string UnitLast = Count + " " + SelectedUnit;
                var updateProduct = new Product
                {
                    Id = ProductParm.Id,
                    Name = Name,
                    Description = Description,
                    Unit = UnitLast
                };
                var response = await _dBService.UpdateProductAsync(updateProduct,_cts.Token);
                if (response.Success == true) _display.ShowMessage(response.Message);
            }
        }
        public void EventInvoke(DataErrorsChangedEventArgs errors)
        {
            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(errors.PropertyName);
        }
        partial void OnNameChanged(string value)
        {
            Debounce(value);
        }
        partial void OnCountChanged(string value)
        {
            _validation.ValidationCount(value);
        }
        public async void Debounce(string value)
        {
            try
            {
                _cts.Cancel();
                _cts = new CancellationTokenSource();
                await Task.Delay(1000, _cts.Token);
                await _validation.EditValidationName(value, ProductParm.Name,_cts.Token);
            }
            catch (OperationCanceledException)
            {

            }
        }
        public void CancelToken()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }
}
