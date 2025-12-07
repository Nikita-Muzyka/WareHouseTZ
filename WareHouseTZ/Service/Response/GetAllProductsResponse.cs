using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Modal;
using System.Collections.ObjectModel;

namespace WareHouseTZ.Service
{
    public class GetAllProductsResponse : DBResponse
    {
        public ObservableCollection<Product> Products { get; set; }
        public GetAllProductsResponse(string message, bool success, ObservableCollection<Product> products) : base(message, success)
        {
            Products = products;
        }
    }
}
