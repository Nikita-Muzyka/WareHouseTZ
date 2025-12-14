using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WareHouseTZ.Modal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;
using WareHouseTZ.Service.Response;
using WareHouseTZ.View;

namespace WareHouseTZ.ViewModal
{
    public partial class ComingViewModel : BaseViewModel
    {
      
        public ObservableCollection<Coming> Comings { get; set; }
        public ObservableCollection<Coming> FilteredComings { get; set; }
        private CancellationTokenSource _cts;

        public ComingViewModel(IDBService dBService, IDisplayService display) : base(dBService, display)
        {
            Comings = new ObservableCollection<Coming>();
            FilteredComings = new ObservableCollection<Coming>();
            _cts = new CancellationTokenSource();
        }
        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private DateTime selectedDateFrom = DateTime.Now.AddMonths(-1);

        [ObservableProperty]
        private DateTime selectedDateTo = DateTime.Now;

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
        public async Task LoadComings()
        {
           var response = await _dBService.GetAllComingDBAsync(_cts.Token);
            if(response.Success == true)
            {
                var getall = response as GetAllComingResponse;
                Comings = getall.Comings;
                FilteredComings = Comings;
            }
        }

        [RelayCommand]
        public async Task EditComing(Coming coming)
        {
          
        }

        [RelayCommand]
        public async Task DeleteComing(Coming coming)
        {
           var response = await _dBService.DeleteComingAsync(coming.Id,_cts.Token);
            _display.ShowMessage(response.Message);
            await LoadComings();
        }

        [RelayCommand]
        public async Task AddComing()
        {
            await Shell.Current.GoToAsync(nameof(CreateComingView));
        }

        public void CancelToken()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }
}