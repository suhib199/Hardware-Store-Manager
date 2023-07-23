using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public virtual ICollection <Invoice> Invoices { get; set; }
    }
}
