using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseTZ.Modal
{
    public class Coming
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Supplier { get; set; }
        public string? Document { get; set; }
    }
}
