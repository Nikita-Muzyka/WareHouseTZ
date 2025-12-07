using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Service
{
    public abstract class DBResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public DBResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
