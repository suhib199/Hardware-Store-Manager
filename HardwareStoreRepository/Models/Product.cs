using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Models
{
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BarcodesId { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual Barcodes Barcodes { get; set; }

    }
}
