using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Modal
{
    public class Consumption
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string? Recipient { get; set; }
        public string? Reason { get; set; }
        public string? Document { get; set; }
    }
}
