using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
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
    public partial class CreateProductViewModal : BaseViewModel
    {
        private ProductValidation _validation;
        private CancellationTokenSource _cts;
        public CreateProductViewModal(IDBService dBService, IDisplayService display) : base(dBService, display)
        {   
            _validation = new ProductValidation(dBService);
            _validation.ErrorsChanged += (s,e) => EventInvoke(e);
            _cts = new CancellationTokenSource();
        }

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

        [RelayCommand]
        public async Task CreateProduct()
        {
            try
            {
                _validation.ValidationAll(Name, Count, _cts.Token);
                if (HasErrors == false)
                {
                    string UnitLast = Count + " " + SelectedUnit;
                    var product = new Product
                    {
                        Name = Name,
                        Description = Description,
                        Created_At = DateTime.Now,
                        Unit = UnitLast
                    };

                    _cts.Token.ThrowIfCancellationRequested();
                    var response = await _dBService.AddProductDBAsync(product, _cts.Token);
                    if (response.Success == true) _display.ShowMessage(response.Message);
                }
            }
            catch (OperationCanceledException) { }
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
                await _validation.ValidationName(value,_cts.Token);
            }
            catch(OperationCanceledException)
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
