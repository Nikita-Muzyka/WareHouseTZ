using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WareHouseTZ.Modal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;

namespace WareHouseTZ.ViewModal
{
    public partial class ComingViewModel : ObservableObject
    {
        private readonly IDBService _dBService;
        private readonly IDisplayService _display;

        public ObservableCollection<Coming> Comings { get; set; }
        public ObservableCollection<Coming> FilteredComings { get; set; }

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private DateTime selectedDateFrom = DateTime.Now.AddMonths(-1);

        [ObservableProperty]
        private DateTime selectedDateTo = DateTime.Now;

        public ComingViewModel(IDBService dBService, IDisplayService display)
        {
            _dBService = dBService;
            _display = display;

            Comings = new ObservableCollection<Coming>();
            FilteredComings = new ObservableCollection<Coming>();

            LoadComings();
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFilter();
        }

        partial void OnSelectedDateFromChanged(DateTime value)
        {
            ApplyFilter();
        }

        partial void OnSelectedDateToChanged(DateTime value)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (Comings == null) return;

            var query = Comings.AsEnumerable();

            // Фильтр по дате
            query = query.Where(c => c.Date.Date >= SelectedDateFrom.Date &&
                                     c.Date.Date <= SelectedDateTo.Date);

            // Фильтр по поиску
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var searchLower = SearchText.ToLowerInvariant();
                query = query.Where(c =>
                    (c.ProductName?.ToLowerInvariant().Contains(searchLower) ?? false) ||
                    (c.Supplier?.ToLowerInvariant().Contains(searchLower) ?? false) ||
                    (c.Document?.ToLowerInvariant().Contains(searchLower) ?? false));
            }

            FilteredComings = new ObservableCollection<Coming>(query);
            OnPropertyChanged(nameof(FilteredComings));
        }

        [RelayCommand]
        public async void LoadComings()
        {
           
        }

        [RelayCommand]
        public async void EditComing(Coming coming)
        {
          
        }

        [RelayCommand]
        public async void DeleteComing(Coming coming)
        {
           
        }

        [RelayCommand]
        public async void AddComing()
        {

        }
       
    }
}