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

namespace WareHouseTZ.ViewModal
{
    public partial class CreateProductViewModal : ObservableObject
    {
        private readonly IDBService _dbService;
        private ProductValidation _validation;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        public CreateProductViewModal(IDBService dBService)
        {
            _dbService = dBService;
            _validation = new ProductValidation(dBService);
            _validation.ErrorsChanged += (s,e) => EventInvoke(e);
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
        public string response;

        [ObservableProperty]
        public List<string> units = new List<string>
        {
            "шт", "кг", "г",
        };

        [RelayCommand]
        public async Task CreateProduct()
        {
            _validation.ValidationAll(Name,Count);
            if(HasErrors == false)
            {
                string UnitLast = Count + " " + SelectedUnit;
                var product = new Product
                {
                    Name = Name,
                    Description = Description,
                    Created_At = DateTime.Now,
                    Unit = UnitLast
                };

                var response = await _dbService.AddProductDBAsync(product);
                Response = response.Message;
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
                await _validation.ValidationNameAsync(value);
            }
            catch(TaskCanceledException ex)
            {

            }
        }

    }
}
