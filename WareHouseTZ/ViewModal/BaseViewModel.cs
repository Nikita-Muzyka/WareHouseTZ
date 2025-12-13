using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Display;

namespace WareHouseTZ.ViewModal
{
    public abstract class BaseViewModel : ObservableObject
    {
        //protected CancellationTokenSource _ctsGeneral;
        protected readonly IDBService _dBService;
        protected readonly IDisplayService _display;
        public BaseViewModel(IDBService dBService, IDisplayService display)
        {
            //_ctsGeneral = new CancellationTokenSource();
            _dBService = dBService;
            _display = display;
        }

        //public void CancelToken()
        //{
        //    _ctsGeneral?.Cancel();
        //    _ctsGeneral?.Dispose();
        //    _ctsGeneral = new CancellationTokenSource();
        //}
    }
}
