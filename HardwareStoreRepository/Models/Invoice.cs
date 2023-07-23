using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Models
{
    public class Invoice
    {
        [Key]
        [Column("InvoiceId")]
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal TotalPrice { get; set; }

        [Column ("InvoiceDate")]
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public virtual ICollection <InvoiceItem>InvoiceItems { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
      

    }
}
