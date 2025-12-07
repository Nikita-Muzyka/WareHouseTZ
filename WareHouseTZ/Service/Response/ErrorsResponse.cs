using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Service
{
    public class ErrorsResponse : DBResponse
    {
        public string? ErrorMessage { get; set; }

        public ErrorsResponse(string errorMessage,string message) : base(message,false)
        {
            ErrorMessage = errorMessage;
        }
        public ErrorsResponse(string message) : base(message, false)
        {
        }
    }
}
