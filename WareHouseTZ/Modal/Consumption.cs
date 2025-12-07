using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Modal
{
    public class Consumption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public string? Recipient { get; set; }
        public string? Reason { get; set; }
        public string? Document { get; set; }
    }
}
