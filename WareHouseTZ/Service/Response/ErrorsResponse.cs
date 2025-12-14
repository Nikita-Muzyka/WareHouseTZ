using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Service
{
    public class ErrorsResponse : DBResponse
    {

        public ErrorsResponse(string message) : base(message,false)
        {
        }
    }
}
