using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WareHouseTZ.Modal;
using System.Threading.Tasks;

namespace WareHouseTZ.Service.Response
{
    public class GetAllComingResponse : DBResponse
    {
        public ObservableCollection<Coming> Comings;

        public GetAllComingResponse(string message,bool success,ObservableCollection<Coming> comings) : base(message, true)
        {
            Comings = comings;
        }
    }
}
