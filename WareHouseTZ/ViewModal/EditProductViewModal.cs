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
    public partial class EditProductViewModal : ObservableObject
    {
        private readonly IDBService _dbService;
        private readonly IDisplayService _displayService;
        public Product _product;
        private ProductValidation _validation;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public bool HasErrors => _validation.HasErrors;
        public string NameError => _validation.GetErrors("NameError") as String;
        public string CountError => _validation.GetErrors("CountError") as String;
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
        public EditProductViewModal(IDBService dBService,IDisplayService display,Product product) 
        {
            _dbService = dBService;
            _displayService = display;
            _product = product;
            _validation = new ProductValidation(dBService);
            _validation.ErrorsChanged += (s, e) => EventInvoke(e);
            LoadProduct();
        }
        void LoadProduct()
        {
            Name = _product.Name;
            Description = _product.Description;

            string text = _product.Unit.TrimEnd(new char[] { ' ','ш','т','к','г' });

            Count = text;
        }

        [RelayCommand]
        public async void EditProduct()
        {
            _validation.EditValidationAll(Name, Count,_product.Name);
            if (HasErrors == false)
            {
                string UnitLast = Count + " " + SelectedUnit;
                var updateProduct = new Product
                {
                    Id = _product.Id,
                    Name = Name,
                    Description = Description,
                    Unit = UnitLast
                };

                var response = await _dbService.UpdateProductAsync(updateProduct);
                if (response.Success == true) _displayService.ShowMessage(response.Message);
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
                _tokenSource.Cancel();
                _tokenSource = new CancellationTokenSource();
                await Task.Delay(1000, _tokenSource.Token);
                _validation.EditValidationName(value,_product.Name);
            }
            catch (TaskCanceledException ex)
            {

            }
        }
    }
}
