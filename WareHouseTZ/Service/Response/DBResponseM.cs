using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Service
{
    public class DBResponseMessage : DBResponse
    {
        public DBResponseMessage(string message, bool success) : base(message, true)
        {

        }
    }
}
