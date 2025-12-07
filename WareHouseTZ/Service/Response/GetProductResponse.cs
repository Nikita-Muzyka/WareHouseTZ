using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Modal;

namespace WareHouseTZ.Service
{
    public class GetProductResponse : DBResponse
    {
        public Product _product { get;}
        public GetProductResponse(string message, bool success, Product product) : base(message, success)
        {
            _product = product;
        }
    }
}
