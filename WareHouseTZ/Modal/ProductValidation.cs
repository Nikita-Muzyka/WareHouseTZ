using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Service;

namespace WareHouseTZ.Modal
{
    public class ProductValidation : INotifyDataErrorInfo
    {
        string propertyNameError = "NameError";
        string propertyCountError = "CountError";
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) return _errors.Values.SelectMany(errors => errors);
            return _errors.ContainsKey(propertyName) ? _errors[propertyName].FirstOrDefault() : Enumerable.Empty<string>();
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly IDBService _dbService;
        public bool HasErrors => _errors.Any();

        public ProductValidation(IDBService dBService)
        {
            _dbService = dBService;
        }
        public void ValidationAll(string Name,string Count)
        {
            ValidationName(Name);
            ValidationCount(Count);
        }
        public void EditValidationAll(string Name, string Count,string OldName)
        {
            EditValidationName(Name,OldName);
            ValidationCount(Count);
        }
        public async void EditValidationName(string Name,string OldName)
        {
            ErrorsClear(propertyNameError);
            if (string.IsNullOrWhiteSpace(Name) == false)
            {
                if (Name.Length < 50)
                {
                    var response = await _dbService.EditCheckNameProductAsync(Name,OldName);
                    if (response.Success == true)
                    {
                        OnErrorsChanged(propertyNameError);
                    }
                    else ErrorsAdd(propertyNameError, response.Message);
                }
                else ErrorsAdd(propertyNameError, "Name должен сожержать не больше 50  знаков");
            }
            else ErrorsAdd(propertyNameError, "Поле обязательно к заполнению");
        }
        public async void ValidationName(string Name)
        {
            ErrorsClear(propertyNameError);
            if (string.IsNullOrWhiteSpace(Name) == false)
            {
                if (Name.Length < 50)
                {
                   var response = await _dbService.CheckNameProductAsync(Name);
                    if (response.Success == true)
                    {
                        OnErrorsChanged(propertyNameError);
                    }
                    else ErrorsAdd(propertyNameError, response.Message);
                }
                else ErrorsAdd(propertyNameError, "Name должен сожержать не больше 50  знаков");
            }
            else ErrorsAdd(propertyNameError, "Поле обязательно к заполнению");
        }
        public void ValidationCount(string Count)
        {
            ErrorsClear(propertyCountError);
            if (string.IsNullOrWhiteSpace(Count) == false)
            {
                if (Count.Any(char.IsNumber) == true && Count.Any(char.IsLetter) == false)
                {
                    OnErrorsChanged(propertyCountError);
                }
                else ErrorsAdd(propertyCountError, "Поле должен сожержать только цифры");
            }
            else ErrorsAdd(propertyCountError, "Поле обязательно к заполнению");
        }
        void ErrorsClear(string PropertyName)
        {
            if (_errors.ContainsKey(PropertyName)) _errors.Remove(PropertyName);
        }
        void ErrorsAdd(string propertyName, string value)
        {
            _errors.Add(propertyName, new List<string> { value });
            OnErrorsChanged(propertyName);
        }
        void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}

