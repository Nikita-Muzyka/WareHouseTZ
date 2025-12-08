using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Service.Display
{
    public class DisplayService : IDisplayService
    {
        public async void ShowMessage(string message)
        {
            await Shell.Current.DisplayAlert(message,"","Ok");
        }
    }
}
