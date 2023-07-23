using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStoreRepository.DTO
{
    public class InvoiceDTOReq
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public int CustomerId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public int EmployeeId { get; set; }


        [RegularExpression("^[0-9]{1,}", ErrorMessage = "InvoiceNumber must consist of numbers only")]
        public int InvoiceNumber { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        [RegularExpression("^\\d+(\\.\\d+)?$", ErrorMessage = "The price must consist of numbers only")]
        public decimal TotalPrice { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public DateTime Date { get; set; }

    }
}
